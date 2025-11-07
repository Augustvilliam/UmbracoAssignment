document.addEventListener("DOMContentLoaded", () => {
    const menuButton = document.querySelector(".phone-nav button");
    const phoneNav = document.querySelector(".nav-phone");

    if (menuButton && phoneNav) {
        menuButton.addEventListener("click", () => {
            phoneNav.classList.toggle("open");
        });
    }
});
