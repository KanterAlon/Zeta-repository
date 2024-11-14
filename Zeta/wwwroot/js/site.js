function openPopup() {
    document.querySelector(".popup-container").style.display = "flex";
}

function closePopup() {
    document.querySelector(".popup-container").style.display = "none";
}

function openPopupArticuloBlog() {
    document.querySelector(".pop-up-info-articulo").style.display = "flex";
    document.querySelector(".sec-nutrition-lifestyle").style.display = "none";
    document.querySelector(".page").style.display = "none";
}

function closePopupArticuloBlog() {
    document.querySelector(".pop-up-info-articulo").style.display = "none";
}

function toggleReaction(button, type) {
    const likeIcon = button.parentElement.querySelector(".img-like");
    const likeFillIcon = button.parentElement.querySelector(".img-like-fill");
    const dislikeIcon = button.parentElement.querySelector(".img-dislike");
    const dislikeFillIcon = button.parentElement.querySelector(".img-dislike-fill");

    if (type === 'like') {
        // Alternar el estado del like
        if (likeIcon.style.display === "none") {
            likeIcon.style.display = "block";
            likeFillIcon.style.display = "none";
        } else {
            likeIcon.style.display = "none";
            likeFillIcon.style.display = "block";

            // Desactivar el dislike si está activo
            dislikeIcon.style.display = "block";
            dislikeFillIcon.style.display = "none";
        }
    } else if (type === 'dislike') {
        // Alternar el estado del dislike
        if (dislikeIcon.style.display === "none") {
            dislikeIcon.style.display = "block";
            dislikeFillIcon.style.display = "none";
        } else {
            dislikeIcon.style.display = "none";
            dislikeFillIcon.style.display = "block";

            // Desactivar el like si está activo
            likeIcon.style.display = "block";
            likeFillIcon.style.display = "none";
        }
    }
}

function openCommentsPopup(button) {
    // Puedes agregar lógica para cargar los comentarios del post correspondiente usando alguna referencia al botón si es necesario.
    document.getElementById("commentsPopup").style.display = "flex";
}

function closeCommentsPopup() {
    document.getElementById("commentsPopup").style.display = "none";
}

function replyToComment() {
    // Lógica para responder a un comentario (puedes personalizar esta parte)
    alert("Función para responder a un comentario.");
}

function publishComment() {
    const newComment = document.getElementById("newCommentInput").value.trim();
    if (newComment !== "") {
        // Crear un nuevo elemento de comentario
        const commentItem = document.createElement("div");
        commentItem.className = "comment-item";
        commentItem.innerHTML = `<p><strong>Tú:</strong> ${newComment}</p><button class="reply-btn" onclick="replyToComment()">Responder</button>`;
        
        // Añadir el nuevo comentario a la lista
        document.getElementById("commentsList").appendChild(commentItem);
        
        // Limpiar el textarea
        document.getElementById("newCommentInput").value = "";
    }
}

function openSharePopup() {
    document.getElementById('sharePopup').style.display = 'flex';

    // Configuración dinámica de las URLs
    const postUrl = window.location.href; // Cambia según la lógica del post
    const whatsappShareUrl = `https://api.whatsapp.com/send?text=Mira este post: ${postUrl}`;
    const smsShareUrl = `sms:?body=Mira este post: ${postUrl}`;
    const mailShareUrl = `mailto:?subject=Mira este post&body=Mira este post: ${postUrl}`;

    document.getElementById('whatsappShare').href = whatsappShareUrl;
    document.getElementById('smsShare').href = smsShareUrl;
    document.getElementById('mailShare').href = mailShareUrl;
}

function closeSharePopup() {
    document.getElementById('sharePopup').style.display = 'none';
}