require('dotenv').config();
const express = require('express');
const app = express();
const port = process.env.PORT || 3000;

// sql server
var sql = require('mssql/msnodesqlv8');
var config = {
      driver: 'msnodesqlv8',
      connectionString: 'Driver={ODBC Driver 17 for SQL Server};Server={FAY\\SQLEXPRESS};Database={Portfolio};Trusted_Connection={yes};',
};

// cors 
const cors = require('cors');
app.use(cors());

app.use(express.json());

// gemini config
const { GoogleGenerativeAI } = require("@google/generative-ai");
const genAI = new GoogleGenerativeAI(process.env.API_KEY);
const model = genAI.getGenerativeModel({ model: "gemini-pro"});

// get all projects

app.get('/projects', (req, res) => {
    sql.connect(config, function (err) {
        if (err) console.log(err);
        var request = new sql.Request();
        request.query('select * from Projects', function (err, recordset) {
            if (err) console.log(err)
            users = recordset.recordset;
            res.json(users);
        });
    });
});


function getProjectMode() {
    return new Promise((resolve, reject) => {
        sql.connect(config, function (err) {
            if (err) {
                console.log(err);
                reject(err);
            }
            var request = new sql.Request();
            request.query('SELECT [Mode] FROM Mode WHERE ID = 1', function (err, recordset) {
                if (err) {
                    console.log(err);
                    reject(err);
                }
                resolve(recordset.recordset);
            });
        });
    });
}


// top 4 projects selected by gemini-pro api

app.get('/topProjects',  (req, res) => {
    // const prompt = "hello world" ;
    // const result = await model.generateContent(prompt);
    // console.log(result.response.text());
    // res.json(result.response.text());
    let allProjects = "";
    let projects ;
    sql.connect(config, function (err) {
        if (err) console.log(err);
        var request = new sql.Request();
        request.query('select * from Projects', async function (err, recordset) {
            if(err) console.log(err);
            projects = recordset.recordset;
            for (let i = 0; i < projects.length; i++) {
                allProjects += "Project ID : " + projects[i].ID + ". Project Title : " + projects[i].Title + ". Tags : " + projects[i].Tags + ". Description : " + projects[i].Description + " .\n ";
            }
            getProjectMode().then(async (data) => {
                queryType = data[0].Mode;
                let prompt = `I am a software developer and I have worked on the following projects.I want you to sort out the best 4 projects I have done using ${queryType} technology. I will give you the ID , Title , Tags and Description of each project and you will return a response with only the id numbers separated by comma. Example response: P1=7, P2=2, P3=4, P4=5 . If there are not enough projects in this topic , add some related topic projects to make it 4 (strictly). You will not reply anything else other than the comma separated ranking. The Projects are \n` + allProjects;

                console.log(prompt);
                const result = await model.generateContent(prompt);
                res.json(result.response.text());
            }).catch((err) => {
                console.log(err);
            });
        });  
    }); 
});





app.get('/image' , (req, res) => {
    try {
        sql.connect(config , function (err) {
            if (err) console.log(err);
            var request = new sql.Request();
            request.query('SELECT Image from Projects where ID=3013;', function (err, recordset) {
                if (err) console.log(err)
                // res.setHeader('Content-Type', 'image/png');
                // res.send(recordset.recordset[0].Image);
                const imageData = recordset.recordset[0].Image;
                const imageBase64 = imageData.toString('base64');
                console.log(imageData);
                res.send(`<img src="data:image/jpeg;base64,${imageBase64}" />`);
            });
        });    
    } catch (err) {
        console.error('Database query failed:', err);
        res.status(500).send('Error retrieving image');
    }
});

// get education

app.get('/education', (req, res) => {
    sql.connect(config, function (err) {
        if (err) console.log(err);
        var request = new sql.Request();
        request.query('select * from Education', function (err, recordset) {
            if (err) console.log(err)
            users = recordset.recordset;
            res.json(users);
        });
    });
});

// get contact

app.get('/contact', (req, res) => {
    sql.connect(config, function (err) {
        if (err) console.log(err);
        var request = new sql.Request();
        request.query('select * from Contact', function (err, recordset) {
            if (err) console.log(err)
            users = recordset.recordset;
            res.json(users);
        });
    });
});

// get skills

app.get('/skills', (req, res) => {
    sql.connect(config, function (err) {
        if (err) console.log(err);
        var request = new sql.Request();
        request.query('select * from Skills', function (err, recordset) {
            if (err) console.log(err)
            users = recordset.recordset;
            res.json(users);
        });
    });
});

// post feedback form data to database
app.post('/feedback', (req, res) => {
    sql.connect(config, function (err) {
        if (err) console.log(err);
        var request = new sql.Request();
        request.query(`INSERT INTO Feedback (Name, Email, Feedback) VALUES ('${req.body.name}', '${req.body.email}', '${req.body.feedback}')`, function (err, recordset) {
            if (err) console.log(err)
            // collect all feedbacks and concat them to a single string
            let feedbacks = "";

            request.query('select * from Feedback', function (err, recordset) {
                if (err) console.log(err)
                users = recordset.recordset;
                for (let i = 0; i < users.length; i++) {
                    feedbacks += (i+1) + ". Feedback : " + users[i].Feedback + " .\n ";
                }

                let prompt = `I am a software developer and I have received the following feedbacks from my clients. I want you to summarize the feedbacks and give me a response within 5 sentences (strictly). Start with "Summary : ". The feedbacks are \n` + feedbacks;
                //console.log(prompt);
                model.generateContent(prompt).then((result) => {
                    let summary = result.response.text();
                    // escape the single quotes
                    summary = summary.replace(/'/g, "''");

                    request.query(`INSERT INTO FeedbackSummary (Summary) VALUES ('${summary}')`, function (err, recordset) {
                        if (err) console.log(err)
                        res.json(summary);
                    });
                }).catch((err) => {
                    console.log(err);
                });
            });
        });
    });
});


    
app.listen(port, () => {
    console.log(`App listening at http://localhost:${port}`);
});

