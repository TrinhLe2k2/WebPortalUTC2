

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

function GetAPIForPageNews(page, filter, record, search) {
    var url = 'https://localhost:7115/Admissions/GetNewsListByPaging?page=';
    url += page;
    url += `&filter=` + filter;
    url += `&record=` + record;
    if (search != '') url += `&search=` + search;
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        },
    })
        .then(response => response.json())
        .then(data => {
            let totalPage = Math.ceil(data.data2nd / record);
            let pageNewslist = document.querySelector('.newsListPage');
            let htmlNewsList = '';
            for (let i = 0; i < data.data.length; i++) {
                let urlImg = (data.data[i].imageObj != null) ? data.data[i].imageObj.relativeUrl : "https://www.survivorsuk.org/wp-content/uploads/2017/01/no-image.jpg";
                htmlNewsList += `
                                            <div>
                                                 <a href="/News/${data.data[i].nameSlug}-${data.data[i].id}" class="news-link text-decoration-none d-block my-3 text-black">
                                                    <div class="row bg-white rounded-4">
                                                        <div class="news-poster position-relative col-3 d-none d-lg-block" style="padding-bottom: 15%;">
                                                            <img src="${urlImg}" class="position-absolute w-100 h-100 rounded-4" style="object-fit: cover;" alt="">
                                                        </div>
                                                        <div class="news-description col-9">
                                                            <div class="ms-3">
                                                                <p class="news-title fw-bold limit-tag-p">${data.data[i].name}</p>
                                                                <p class="news-content limit-tag-p">${data.data[i].description}</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </a>
                                            </div>
                                    `;
            }
            pageNewslist.innerHTML = htmlNewsList;
            let pageNews = document.querySelector('.PageNews');
            let p = pageNews.querySelector('.pageCurrent').innerHTML = `Page ${page} / ${totalPage}`;
            let navUl = pageNews.querySelector('nav ul');
            let html = '';
            if (page > 1) {
                html += `<li class="page-item"> <a class="page-link cursorPointer" onclick="handleClickPage(this)" data-page="${page - 1}"> previous </a></li>`;
            }
            for (let i = 1; i <= totalPage; i++) {
                if (i == page) {
                    html += `<li class="page-item"> <a class="page-link disabled bg-primary" href = "#">${i}</a></li>`;
                }
                else {
                    html += `<li class="page-item"> <a class="page-link cursorPointer" onclick="handleClickPage(this)" data-page="${i}">${i}</a></li>`;
                }
            }
            if (page < totalPage) {
                html += `<li class="page-item" ><a class="page-link cursorPointer" onclick="handleClickPage(this)" data-page="${++page}">next</a></li>`;
            }
            navUl.innerHTML = html;
        })
        .catch(error => {
            console.error('Error:', error);
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