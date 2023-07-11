
const accordionItems = document.querySelectorAll('.accordion-item');
const activeHover = document.querySelectorAll(".hoverActive");
const SubMenu = document.getElementById("subMenuHM");
const SubMenus = document.getElementById("subMenus");

function toggleAccordion() {
  const content = this.nextElementSibling;
  const image = this.querySelector('img');

  this.classList.toggle('active');
  image.classList.toggle('rotate');

  if (content.style.maxHeight) {
    content.style.maxHeight = null;
    content.style.padding = '0';
    image.style.transform = 'rotate(0deg)';
    activeHover.forEach(item => {
      item.classList.remove("activeNavbar");
    });
  } else {
    content.style.maxHeight = '212px';
    content.style.padding = '14px 15px';
    image.style.transform = 'rotate(90deg)';
    activeHover.forEach(item => {
      if (item !== this.parentElement) {
        item.classList.remove("activeNavbar");
      }
    }, this.parentElement);
    this.parentElement.classList.toggle("activeNavbar");
  }
}

accordionItems.forEach(item => {
  const header = item.querySelector('.accordion-header');
  const content = item.querySelector('.accordion-content');

  content.style.maxHeight = null;
  content.style.padding = '0';

  header.addEventListener('click', toggleAccordion);
});

activeHover.forEach(element => {
  element.addEventListener("click", function() {
    activeHover.forEach(item => {
      if (item !== element) {
        item.classList.remove("activeNavbar");
      }
    });
    element.classList.add("activeNavbar");
  });
});

activeHover.forEach(e => {
  e.addEventListener("click", function(e) {
    e.stopPropagation();
    SubMenu.style.display = "block";
    document.body.classList.add('no-scroll');
  });
});

window.addEventListener("click", function(e) {
  if (!SubMenu.contains(e.target)) {
    SubMenu.style.display = "none";
    activeHover.forEach(element => {
      element.classList.remove("activeNavbar");
    });
    document.body.classList.remove('no-scroll');
  }
  if (!SubMenus.contains(e.target)) {
    SubMenu.style.display = "none";
    document.body.classList.remove('no-scroll');
  }
});

var HamburgerMenu = document.querySelector(".HamburgerMenu");
var navbarHamburger = document.getElementById("smallAndMediumNavBar");

HamburgerMenu.addEventListener("click", function(e) {
  e.stopPropagation();
  navbarHamburger.style.display = "block";
  document.body.classList.add('no-scroll');
});

navbarHamburger.addEventListener("click", function(e) {
  e.stopPropagation();
});

navbarHamburger.addEventListener("touchstart", function(e) {
  e.stopPropagation();
});

var menuTitle = document.querySelector(".menuTitle");

menuTitle.addEventListener("click", function(e) {
  e.stopPropagation();
  SubMenus.style.display = "none";
  document.body.classList.remove('no-scroll');
});

var hoverActives = document.querySelectorAll(".hoverActives");

hoverActives.forEach(function(e) {
  e.addEventListener("click", function(es) {
    es.stopPropagation();
    SubMenus.style.display = "block";
    document.body.classList.add('no-scroll');
  });
});

SubMenus.addEventListener("click", function(e) {
  e.stopPropagation();
});

window.addEventListener("click", function(e) {
  if (!navbarHamburger.contains(e.target)) {
    navbarHamburger.style.display = "none";
    document.body.classList.remove('no-scroll');
  }
  if (!menuTitle.contains(e.target) && !SubMenus.contains(e.target)) {
    SubMenus.style.display = "none";
    document.body.classList.remove('no-scroll');
  }
});

menuTitle.addEventListener("click", function(e) {
  e.stopPropagation();
  SubMenus.style.display = "none";
  document.body.classList.remove('no-scroll');
});

document.addEventListener("click", function(e) {
  if (!menuTitle.contains(e.target) && !SubMenus.contains(e.target)) {
    SubMenus.style.display = "none";
    document.body.classList.remove('no-scroll');
  }
});
