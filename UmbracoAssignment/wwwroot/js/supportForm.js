document.addEventListener("DOMContentLoaded", () => {
    const supportForm = document.getElementById("supportForm");
    if (!supportForm) return; 

    supportForm.addEventListener("keydown", (event) => {
        if (event.key === "Enter" && event.target.tagName.toLowerCase() === "input") {
            event.preventDefault();
            supportForm.submit();
        }
    });
});
