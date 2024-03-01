// domcontent loaded

document.addEventListener('DOMContentLoaded', function() {
    const url = window.location.href;
    const lastPortion = url.substring(url.lastIndexOf("/") + 1);

    const listItems = document.querySelectorAll(".navUl li");
    console.log(lastPortion);
    listItems.forEach((item) => {
        const link = item.querySelector("a");
        const href = link.getAttribute("href");
        console.log(href);
        if (href === lastPortion) {
            link.classList.add("activeC");
        } else {
            link.classList.remove("activeC");
        }
    });
});

