let container = document.querySelector(".container");


container.addEventListener("click", function (event) {
    let elem = event.target;
    if (elem.classList.contains("deleteButt")) {
        showConfirmation(elem);
    } else if (elem.classList.contains("delDecline")) {
        hideConfirmation(elem);
    }
});

function showConfirmation(elem) {
    let confirmationPanel = document.querySelector("#delConfirmation" + elem.dataset.id);
    if (!confirmationPanel.classList.contains("delConfirmationShow")) {
        confirmationPanel.classList.add("delConfirmationShow");
    }
    console.log(confirmationPanel);
}

function hideConfirmation(elem) {
    let confirmationPanel = elem.parentNode.parentNode;
    if (confirmationPanel.classList.contains("delConfirmationShow")) {
        confirmationPanel.classList.remove("delConfirmationShow");
    }
}