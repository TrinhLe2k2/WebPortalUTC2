

Start()

function Start() {
    Certification();
}

// Layout js
function ResetActiveNavLink() {
    let navLinks = document.querySelectorAll('.navigation .nav-link');
    navLinks.forEach(item => {
        item.classList.remove('active');
    });
}

// index js

function ModalImage(item) {
    let modal = item.querySelector('.modal');
    let img = item.querySelector('.myImg');
    let modalImg = item.querySelector('.modal img');
    let captionText = item.querySelector('.modal .caption');
    let closeBtn = item.querySelector('.modal .close');

    img.onclick = function () {
        modal.style.display = "block";
        modalImg.src = this.src;
        captionText.innerHTML = this.alt;
    }
    closeBtn.onclick = function () {
        modal.style.display = "none";
    }
    modal.onclick = function (event) {
        if (event.target === modalImg) {

        } else {
            modal.style.display = "none";
        }
    }

}

function Certification() {
    let certification = document.querySelector('.main__certification');
    let cols = certification.querySelectorAll('.main__certification-item');
    cols.forEach(col => {
        ModalImage(col);
    });
}

// introduction