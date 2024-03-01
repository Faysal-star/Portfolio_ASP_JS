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

// gsap scrolltrigger
gsap.registerPlugin(ScrollTrigger);

const splitT = document.querySelectorAll('.disp')

splitT.forEach((el) => {
    let split = new SplitType(el, {type: 'lines, words, chars'})
    let lines = split.lines
    let tl = gsap.timeline({
        scrollTrigger: {
        trigger: el,
        start: 'top 80%',
        end: 'bottom 20%',
        scrub: 1,
        markers: 0,
        }
    })
    tl.from(lines, {opacity: 0, y: 100, duration: 1, stagger: 0.1})
    })


    const splitT2 = document.querySelectorAll('.disp2')
    
    splitT2.forEach((el) => {
        let split = new SplitType(el, {type: 'words, chars'})
        let lines = split.words
        let tl = gsap.timeline({
            scrollTrigger: {
            trigger: el,
            start: 'top 80%',
            end: 'bottom 60%',
            scrub: 1,
            markers: 0,
            }
        })
        tl.from(lines, {opacity: 0.05, y: 15, duration: 1, stagger: 0.1})
        })


const lenis = new Lenis()

lenis.on('scroll', (e) => {
  console.log(e)
})

function raf(time) {
  lenis.raf(time)
  requestAnimationFrame(raf)
}

requestAnimationFrame(raf)