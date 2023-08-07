/*=========================================================================================
    File Name: app-ecommerce.js
    Description: Ecommerce pages js
    ----------------------------------------------------------------------------------------
    
    
    
==========================================================================================*/

'use strict';

$(function () {
  // RTL Support
  var direction = 'ltr';
  if ($('html').data('textdirection') == 'rtl') {
    direction = 'rtl';
  }

  var sidebarShop = $('.sidebar-shop'),
    btnCart = $('.btn-cart'),
    overlay = $('.body-content-overlay'),
    sidebarToggler = $('.shop-sidebar-toggler'),
    gridViewBtn = $('.grid-view-btn'),
    listViewBtn = $('.list-view-btn'),
    presentationViewBtn = $('.presentation-view-btn'),
    //priceSlider = document.getElementById('price-slider'),
    ecommerceProducts = $('#ecommerce-products'),
    sortingDropdown = $('.dropdown-sort .dropdown-item'),
    sortingText = $('.dropdown-toggle .active-sorting'),
    sortingDropdownType = $('.dropdown-sort-type .dropdown-item'),
    sortingTextType = $('.dropdown-toggle .active-sorting-type'),
    sortingDropdownSize = $('.dropdown-sort-size .dropdown-item'),
    sortingTextSize = $('.dropdown-toggle .active-sorting-size'),
    sortingDropdownColour = $('.dropdown-sort-colour .dropdown-item'),
    sortingTextColour = $('.dropdown-toggle .active-sorting-colour'),
    sortingDropdownMaterial = $('.dropdown-sort-material .dropdown-item'),
    sortingTextMaterial = $('.dropdown-toggle .active-sorting-material'),
    sortingDropdownShape = $('.dropdown-sort-shape .dropdown-item'),
    sortingTextShape = $('.dropdown-toggle .active-sorting-shape'),
    wishlist = $('.btn-wishlist'),
    checkout = 'app-ecommerce-checkout.html';

  if ($('body').attr('data-framework') === 'laravel') {
    var url = $('body').attr('data-asset-path');
    checkout = url + 'app/ecommerce/checkout';
  }

  // On sorting dropdown change
  if (sortingDropdown.length) {
    sortingDropdown.on('click', function () {
      var $this = $(this);
      var selectedLang = $this.text();
      sortingText.text(selectedLang);
    });
  }

  if (sortingDropdownType.length) {
    sortingDropdownType.on('click', function () {
      var $this = $(this);
      var selectedLang = $this.text();
      sortingTextType.text(selectedLang);
    });
  }

  if (sortingDropdownSize.length) {
    sortingDropdownSize.on('click', function () {
      var $this = $(this);
      var selectedLang = $this.text();
      sortingTextSize.text(selectedLang);
    });
  }

  if (sortingDropdownColour.length) {
    sortingDropdownColour.on('click', function () {
      var $this = $(this);
      var selectedLang = $this.text();
      sortingTextColour.text(selectedLang);
    });
  }

  if (sortingDropdownMaterial.length) {
    sortingDropdownMaterial.on('click', function () {
      var $this = $(this);
      var selectedLang = $this.text();
      sortingTextMaterial.text(selectedLang);
    });
  }

  if (sortingDropdownShape.length) {
    sortingDropdownShape.on('click', function () {
      var $this = $(this);
      var selectedLang = $this.text();
      sortingTextShape.text(selectedLang);
    });
  }

  // Show sidebar
  if (sidebarToggler.length) {
    sidebarToggler.on('click', function () {
      sidebarShop.toggleClass('show');
      overlay.toggleClass('show');
      $('body').addClass('modal-open');
    });
  }

  // Overlay Click
  if (overlay.length) {
    overlay.on('click', function (e) {
      sidebarShop.removeClass('show');
      overlay.removeClass('show');
      $('body').removeClass('modal-open');
    });
  }

 
  // List View
  if (listViewBtn.length) {
    listViewBtn.on('click', function () {
      ecommerceProducts.removeClass('grid-view presentation-view').addClass('list-view');
      [gridViewBtn,presentationViewBtn].removeClass('active');
      listViewBtn.addClass('active');
    });
  }

  // Grid View
  if (gridViewBtn.length) {
    gridViewBtn.on('click', function () {
      ecommerceProducts.removeClass('list-view presentation-view').addClass('grid-view');
      [listViewBtn,presentationViewBtn].removeClass('active');
      gridViewBtn.addClass('active');
    });
  }

  // Presentation View
  if (presentationViewBtn.length) {
    presentationViewBtn.on('click', function () {
      ecommerceProducts.removeClass('grid-view list-view').addClass('presentation-view');
      [gridViewBtn,listViewBtn].removeClass('active');
      presentationViewBtn.addClass('active');
    });
  }

  // On cart & view cart btn click to cart
  if (btnCart.length) {
    btnCart.on('click', function (e) {
      var $this = $(this),
        addToCart = $this.find('.add-to-cart');
      if (addToCart.length > 0) {
        e.preventDefault();
      }
      addToCart.text('View In Cart').removeClass('add-to-cart').addClass('view-in-cart');
      $this.attr('href', checkout);
      toastr['success']('', 'Added Item In Your Cart 🛒', {
        closeButton: true,
        tapToDismiss: false
      });
    });
  }

  // For Wishlist Icon
  if (wishlist.length) {
    wishlist.on('click', function () {
      var $this = $(this);
      $this.find('svg').toggleClass('text-danger');
      if ($this.find('svg').hasClass('text-danger')) {
        toastr['success']('', 'Added to wishlist ❤️', {
          closeButton: true,
          tapToDismiss: false
        });
      }
    });
  }
});

// on window resize hide sidebar
$(window).on('resize', function () {
  if ($(window).outerWidth() >= 991) {
    $('.sidebar-shop').removeClass('show');
    $('.body-content-overlay').removeClass('show');
  }
});
