const searchInput = document.getElementById('menuSearch');
const searchResultCard = document.getElementById('searchResultCard');
const searchResultList = document.getElementById('searchResultList');
const allLinks = document.querySelectorAll('.menu-item-source');

searchInput.addEventListener('input', function (e) {
    const val = e.target.value.toLowerCase().trim();
    searchResultList.innerHTML = '';
    if (val.length === 0) { searchResultCard.style.display = 'none'; return; }

    let matches = 0;
    allLinks.forEach(link => {
        const name = link.getAttribute('data-name');
        if (name.toLowerCase().includes(val)) {
            const clone = link.cloneNode(true);
            clone.style.display = 'flex';
            searchResultList.appendChild(clone);
            matches++;
        }
    });

    searchResultCard.style.display = 'block';
    if (matches === 0) searchResultList.innerHTML = '<div class="text-muted p-3 text-center">No matches found</div>';
});

window.addEventListener('click', (e) => {
    if (!e.target.closest('.search-container')) searchResultCard.style.display = 'none';
});

// TOGGLE PROFILE
function toggleProfile(e) {
    e.stopPropagation();
    document.getElementById('profileMenu').classList.toggle('show');
}

window.onclick = function (event) {
    if (!event.target.closest('.user-sec')) {
        document.getElementById('profileMenu').classList.remove('show');
    }
}

// SMART POSITIONING
document.querySelectorAll('.nav-item').forEach(item => {
    item.addEventListener('mouseenter', function () {
        const menu = this.querySelector('.form-menu');
        if (menu) {
            const rect = menu.getBoundingClientRect();
            if (rect.right > window.innerWidth) {
                menu.style.left = 'auto';
                menu.style.right = '0';
            }
        }
    });
});

// LOADER
window.onbeforeunload = () => { document.getElementById('top-loader').style.width = "50%"; };
document.addEventListener("DOMContentLoaded", () => {
    const l = document.getElementById('top-loader');
    l.style.width = "100%"; setTimeout(() => l.style.opacity = "0", 400);
});
