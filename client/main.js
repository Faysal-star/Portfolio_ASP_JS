let code = `` 
fetch('code.txt')
  .then(response => response.text())
  .then(data => {displayCode(data);});
function displayCode(data) {
  let editor = CodeMirror(document.getElementById("editor"), {
    value: data,
    mode: "javascript",
    theme: "material-ocean",
    lineNumbers: true,
    readOnly: true,
  });
  editor.getWrapperElement().classList.add('editor2');
  editor.getWrapperElement().style.background = '#ffffff00';
}

gsap.registerPlugin(ScrollTrigger);


const splitT2 = document.querySelectorAll('.disp2')

splitT2.forEach((el) => {
  let split = new SplitType(el, {type: 'words, chars'})
  let lines = split.words
  let tl = gsap.timeline({
      scrollTrigger: {
      trigger: el,
      start: 'top 80%',
      end: 'bottom 70%',
      scrub: 1,
      markers: 0,
      }
    })
    tl.from(lines, {opacity: 0.05, y: 15, duration: 1, stagger: 0.1})
})


const lenis = new Lenis()

lenis.on('scroll', (e) => {
  // console.log(e)
})

function raf(time) {
  lenis.raf(time)
  requestAnimationFrame(raf)
}

requestAnimationFrame(raf)


// education section

let prefix = 'http://localhost'
let prefix2 = 'http://192.168.0.101'
let urlEdu = `${prefix}:3000/education`

function loadEducation(){
  return new Promise((resolve, reject) => {
    fetch(urlEdu)
    .then(response => response.json())
    .then(data => {
      displayEducation(data);
    }); 
    resolve()
  })
}

function displayEducation(data) {
  let eduLine = document.querySelector('.eduLine')

  data.forEach((el, i) => {
    // console.log(el.Degree);
    let childDiv = 
    `<div class="point">
      <div>
        <h2 class="degree">${el.Degree}</h2>
        <h2 class="insti">${el.Institution}</h2>
        <h3 class="year">${el.Graduation}</h3>
      </div>
    </div>`

    eduLine.innerHTML += childDiv
  })
}

// Skill section

let urlSkill = `${prefix}:3000/skills`

// [{"ID":1,"Skill":"C++","Progress":50}]

function loadSkill(){
  return new Promise((resolve, reject) => {
    fetch(urlSkill)
    .then(response => response.json())
    .then(data => {
      displaySkill(data);
    }); 
    resolve();
  }
  );
}

function displaySkill(data) {
  let skillLine = document.querySelector('.allSkill')

  data.forEach((el, i) => {
    // console.log(el.Degree);
    let childLi = 
    `<li>
      <h3>${el.Skill}</h3>
      <span class="bar"><span class="${el.Skill}" style="width: ${el.Progress}%; animation : ${el.Skill} 1s ease-in-out 0s 1 normal forwards;"></span></span>
    </li>`

    skillLine.innerHTML += childLi
  })
}

let urlProject = `${prefix}:3000/topProjects`
let urlProjects = `${prefix}:3000/projects`

function loadProject(){
  return new Promise((resolve, reject) => {
    fetch(urlProject)
    .then(response => response.json())
    .then(data => {
      displayProject(data);
    }); 
    resolve();
  });
}

function displayProject(dataa) {
  let dataSplit = dataa.split(',');
  let projectID = [];

  for (let i = 0; i < dataSplit.length; i++) {
    projectID.push(dataSplit[i].split('=')[1]);
  }

  if (projectID.length < 1) {
    projectID.push('3012', '3013', '3014', '3011');
  }

  console.log(projectID);

  fetch(urlProjects)
    .then(response => response.json())
    .then(data => {
      let projectLine = document.querySelector('.allProject');

      for (let i = 0; i < data.length; i++) {
        let el = data[i];

        if (projectID.includes(el.ID.toString())) {
          let imageData = el.Image.data;
          let imageBase64 = btoa(
            new Uint8Array(imageData)
              .reduce((data, byte) => data + String.fromCharCode(byte), '')
          );;

          let childDiv =
            `<div class="project">
              <div class="prdesc">
                <div class="upper">
                  <div class="primg">
                    <img src="data:image/jpeg;base64,${imageBase64}" alt="pro pic">
                  </div>
                  <div class="prlink">
                    <h3>${el.Title}</h3>
                    <a href="${el.URL}" class="code" target="_blank">
                      <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 20 20"><path fill="currentColor" fill-rule="evenodd" d="M12.316 3.051a1 1 0 0 1 .633 1.265l-4 12a1 1 0 1 1-1.898-.632l4-12a1 1 0 0 1 1.265-.633M5.707 6.293a1 1 0 0 1 0 1.414L3.414 10l2.293 2.293a1 1 0 1 1-1.414 1.414l-3-3a1 1 0 0 1 0-1.414l3-3a1 1 0 0 1 1.414 0m8.586 0a1 1 0 0 1 1.414 0l3 3a1 1 0 0 1 0 1.414l-3 3a1 1 0 1 1-1.414-1.414L16.586 10l-2.293-2.293a1 1 0 0 1 0-1.414" clip-rule="evenodd"/></svg>
                    </a>
                    <p class="tags">${el.Tags}</p>
                  </div>
                </div>
                <div class="lower">
                  <p>${el.Description}</p>
                </div>
              </div>
            </div>`;

          projectLine.innerHTML += childDiv;
        }
      }
    });
}

// load primary contact 
// response : [{"ID":1,"Address":"Dhaka","Mobile":"01797363491","Email":"a@gmail.com","Type":"Primary"}]

let social = `<a href="facebook.com" class="fb"><svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 24 24"><path fill="currentColor" d="M22 12c0-5.52-4.48-10-10-10S2 6.48 2 12c0 4.84 3.44 8.87 8 9.8V15H8v-3h2V9.5C10 7.57 11.57 6 13.5 6H16v3h-2c-.55 0-1 .45-1 1v2h3v3h-3v6.95c5.05-.5 9-4.76 9-9.95"/></svg></a>
<a href="linkedin.com" class="linkedin"><svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 24 24"><path fill="currentColor" d="M19 3a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2zm-.5 15.5v-5.3a3.26 3.26 0 0 0-3.26-3.26c-.85 0-1.84.52-2.32 1.3v-1.11h-2.79v8.37h2.79v-4.93c0-.77.62-1.4 1.39-1.4a1.4 1.4 0 0 1 1.4 1.4v4.93zM6.88 8.56a1.68 1.68 0 0 0 1.68-1.68c0-.93-.75-1.69-1.68-1.69a1.69 1.69 0 0 0-1.69 1.69c0 .93.76 1.68 1.69 1.68m1.39 9.94v-8.37H5.5v8.37z"/></svg></a>
<a href="github.com" class="git"><svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 24 24"><path fill="currentColor" d="M12 2A10 10 0 0 0 2 12c0 4.42 2.87 8.17 6.84 9.5c.5.08.66-.23.66-.5v-1.69c-2.77.6-3.36-1.34-3.36-1.34c-.46-1.16-1.11-1.47-1.11-1.47c-.91-.62.07-.6.07-.6c1 .07 1.53 1.03 1.53 1.03c.87 1.52 2.34 1.07 2.91.83c.09-.65.35-1.09.63-1.34c-2.22-.25-4.55-1.11-4.55-4.92c0-1.11.38-2 1.03-2.71c-.1-.25-.45-1.29.1-2.64c0 0 .84-.27 2.75 1.02c.79-.22 1.65-.33 2.5-.33c.85 0 1.71.11 2.5.33c1.91-1.29 2.75-1.02 2.75-1.02c.55 1.35.2 2.39.1 2.64c.65.71 1.03 1.6 1.03 2.71c0 3.82-2.34 4.66-4.57 4.91c.36.31.69.92.69 1.85V21c0 .27.16.59.67.5C19.14 20.16 22 16.42 22 12A10 10 0 0 0 12 2"/></svg></a>` ;

let urlContact = `${prefix}:3000/contact`

function loadContact(){
  return new Promise((resolve, reject) => {
    fetch(urlContact)
    .then(response => response.json())
    .then(data => {
      displayContact(data);
    }); 
    resolve();
  });
}

function displayContact(data) {
  let contactLine = document.querySelector('.contact')
  let childDiv = '';
  data.forEach((el, i) => {
    if(el.Type === 'Primary'){
      childDiv += 
        `<p class="add">${el.Address}</p>
        <p class="mobile">${el.Mobile}</p>
        <p class="email">${el.Email}</p>`;
    }
  })
  childDiv += social
  contactLine.innerHTML += childDiv
}

//send feedback 

let urlFeedback = `${prefix}:3000/feedback`

let fbtn = document.querySelector('#fbtn');

fbtn.addEventListener('click', (e) => {
  e.preventDefault();
  let name = document.querySelector('#fname').value;
  let email = document.querySelector('#femai').value;
  let message = document.querySelector('#feedback').value;

  let data = {
    name: name,
    email: email,
    feedback: message
  }
  console.log(data);

  fetch(urlFeedback, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(data)
  })
  .then(response => response.json())
  .then(data => {
    console.log('Success:', data);
  })
  .catch((error) => {
    console.error('Error:', error);
  });

  document.querySelector('#fname').value = '';
  document.querySelector('#femai').value = '';
  document.querySelector('#feedback').value = '';

})


async function loadAll(){
  await loadEducation();
  await loadSkill();
  await loadProject();
  await loadContact();
}

loadAll();