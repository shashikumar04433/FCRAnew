/*=========================================================================================
    File Name: form-repeater.js
    Description: form repeater page specific js
    ----------------------------------------------------------------------------------------
    
    Version: 1.0
    
    
==========================================================================================*/

$(function () {
  'use strict';

  // form repeater jquery
  $('.invoice-repeater, .repeater-default').repeater({
    show: function () {
      $(this).slideDown();
      // Feather Icons
      if (feather) {
        feather.replace({ width: 14, height: 14 });
      }
    },
    hide: function (deleteElement) {
      if (confirm('Are you sure you want to delete this element?')) {
        $(this).slideUp(deleteElement);
      }
    }
  });
});





$('.bathrooms').on('click', '.remove', function () {
  var tr = $(this).closest('tr').remove();
  $(tr).deleteAfter($(this).closest('tr'));
});
$('.bathrooms').on('click', '.clone', function () {
  var tr = $(this).closest('tr').clone();
  $(tr).insertAfter($(this).closest('tr'));
  $(".bathrooms .btn-danger").not(':first').last().addClass("d-none");
});

$('.woodfloor').on('click', '.remove', function () {
  var tr = $(this).closest('tr').remove();
  $(tr).deleteAfter($(this).closest('tr'));
});
$('.woodfloor').on('click', '.clone', function () {
  var tr = $(this).closest('tr').clone();
  $(tr).insertAfter($(this).closest('tr'));
  $(".woodfloor .btn-danger").not(':first').last().addClass("d-none");
});

$('.kitchens').on('click', '.remove', function () {
  var tr = $(this).closest('tr').remove();
  $(tr).deleteAfter($(this).closest('tr'));
});
$('.kitchens').on('click', '.clone', function () {
  var tr = $(this).closest('tr').clone();
  $(tr).insertAfter($(this).closest('tr'));
  $(".kitchens .btn-danger").not(':first').last().addClass("d-none");
});















// $('.wrapper').on('click', '.remove', function() {
//   $('.remove').closest('.wrapper').find('.element').not(':first').last().remove();
// });
// $('.wrapper').on('click', '.clone', function() {
//   $('.clone').closest('.wrapper').find('.element').first().clone().appendTo('.results');
// });