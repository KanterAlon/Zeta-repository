function openPopup() {
    document.getElementById("postPopup").style.display = "flex";
}

function closePopup() {
    document.getElementById("postPopup").style.display = "none";
}

function openPopupArticuloBlog() {
    document.querySelector(".pop-up-info-articulo").style.display = "flex";
    document.querySelector(".sec-nutrition-lifestyle").style.display = "none";
    document.querySelector(".page").style.display = "none";
}

function closePopupArticuloBlog() {
    document.querySelector(".pop-up-info-articulo").style.display = "none";
}