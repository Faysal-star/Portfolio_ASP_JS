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

// get users

let users = [] ;
app.get('/', (req, res) => {
    res.send('Hello World!');
});

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

app.get('/image' , (req, res) => {
    try {
        sql.connect(config , function (err) {
            if (err) console.log(err);
            var request = new sql.Request();
            request.query('SELECT Image from Projects where ID=2;', function (err, recordset) {
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


app.listen(port, () => {
    console.log(`Example app listening at http://localhost:${port}`);
});

