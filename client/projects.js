let prefix = 'localhost'
let prefix2 = 'http://192.168.0.101'
let urlProjects = `${prefix2}:3000/projects`
fetch(urlProjects)
.then(response => response.json())
.then(data => {
  let projectLine = document.querySelector('.allProject');
  for (let i = 0; i < data.length; i++) {
    let el = data[i];
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
});