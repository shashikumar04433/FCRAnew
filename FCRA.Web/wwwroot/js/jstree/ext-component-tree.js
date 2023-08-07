/*=========================================================================================
	File Name: ext-component-tree.js
	Description: Tree
==========================================================================================*/

$(function () {
  'use strict';

  var basicTree = $('#products,#projects,#reports,#catalogues,#brands');


  if (basicTree.length) {
    basicTree.jstree();
  }

});
