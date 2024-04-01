let prevScrollpos = window.pageYOffset;

window.onscroll = function() {
    let currentScrollPos = window.pageYOffset;

    if (prevScrollpos > currentScrollPos) {
        document.getElementById("navigationBar").style.top = "0";
    } else {
        document.getElementById("navigationBar").style.top = "-50px";
    }

    prevScrollpos = currentScrollPos;
};