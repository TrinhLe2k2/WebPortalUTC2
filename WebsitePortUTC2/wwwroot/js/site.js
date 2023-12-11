

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

function ActiveNavLink(clasNavLink) {
    document.querySelector(clasNavLink).classList.add('active');
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


// Layout Admin
function Active(collapseNews, collapseNewsList) {
    var navItem = document.querySelector(collapseNews);
    var navLink = navItem.querySelector('.nav-link');
    navLink.classList.remove('collapsed');
    navLink.setAttribute('aria-expanded', 'true');

    navItem.querySelector('.collapse').classList.add('show');
    navItem.querySelector(collapseNewsList).classList.add('active');
}