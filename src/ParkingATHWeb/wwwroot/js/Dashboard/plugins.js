/*================================================================================
  Item Name: Materialize - Material Design Admin Template
  Version: 3.1
  Author: GeeksLabs
  Author URL: http://www.themeforest.net/user/geekslabs
================================================================================*/

$(function() {

  "use strict";

  //var window_width = $(window).width();

  /*Preloader*/
  $(window).load(function() {
    setTimeout(function() {
      $('body').addClass('loaded');      
    }, 200);
  });  

  //// Check first if any of the task is checked
  //$('#task-card input:checkbox').each(function() {
  //  checkbox_check(this);
  //});

  //// Task check box
  //$('#task-card input:checkbox').change(function() {
  //  checkbox_check(this);
  //});

  //// Check Uncheck function
  //function checkbox_check(el){
  //    if (!$(el).is(':checked')) {
  //        $(el).next().css('text-decoration', 'none'); // or addClass            
  //    } else {
  //        $(el).next().css('text-decoration', 'line-through'); //or addClass
  //    }    
  //}

  ///*----------------------
  //* Plugin initialization
  //------------------------*/
  
  //$('select').material_select();
  //// Set checkbox on forms.html to indeterminate
  //var indeterminateCheckbox = document.getElementById('indeterminate-checkbox');
  //if (indeterminateCheckbox !== null)
  //  indeterminateCheckbox.indeterminate = true;
      
  //// Materialize Slider
  //$('.slider').slider({
  //  full_width: true
  //});

  //// Materialize Dropdown
  //$('.dropdown-button').dropdown({
  //  inDuration: 300,
  //  outDuration: 125,
  //  constrain_width: true, // Does not change width of dropdown to that of the activator
  //  hover: false, // Activate on click
  //  alignment: 'left', // Aligns dropdown to left or right edge (works with constrain_width)
  //  gutter: 0, // Spacing from edge
  //  belowOrigin: true // Displays dropdown below the button
  //});
  //// Translation Dropdown
  //$('.translation-button').dropdown({
  //    inDuration: 300,
  //    outDuration: 225,
  //    constrain_width: false, // Does not change width of dropdown to that of the activator
  //    hover: true, // Activate on hover
  //    gutter: 0, // Spacing from edge
  //    belowOrigin: true, // Displays dropdown below the button
  //    alignment: 'left' // Displays dropdown with edge aligned to the left of button
  //  }
  //);
  //// Notification Dropdown
  //$('.notification-button').dropdown({
  //    inDuration: 300,
  //    outDuration: 225,
  //    constrain_width: false, // Does not change width of dropdown to that of the activator
  //    hover: true, // Activate on hover
  //    gutter: 0, // Spacing from edge
  //    belowOrigin: true, // Displays dropdown below the button
  //    alignment: 'left' // Displays dropdown with edge aligned to the left of button
  //  }
  //);

  // Materialize Tabs
  $('.tab-demo').show().tabs();
  $('.tab-demo-active').show().tabs();

  //// Materialize Parallax
  //$('.parallax').parallax();
  //// Materialize Modal
  //$('.modal-trigger').leanModal({
  //    dismissible: true, // Modal can be dismissed by clicking outside of the modal
  //    opacity: .5, // Opacity of modal background
  //    in_duration: 300, // Transition in duration
  //    out_duration: 200, // Transition out duration
  //    ready: function() { 
  //    //alert('Ready'); 
  //    }, // Callback for Modal open
  //    complete: function() { 
  //    //alert('Closed'); 
  //    } // Callback for Modal close
  //});

  // Materialize scrollSpy
  $('.scrollspy').scrollSpy();

  // Materialize tooltip
  $('.tooltipped').tooltip({
    delay: 50
  });

  // Materialize sideNav  

  //Main Left Sidebar Menu
  $('.sidebar-collapse').sideNav({
    edge: 'left', // Choose the horizontal origin    
  });
  
  // Perfect Scrollbar
  $('select').not('.disabled').material_select();
    var leftnav = $(".page-topbar").height();  
    var leftnavHeight = window.innerHeight - leftnav;
  $('.leftside-navigation').height(leftnavHeight).perfectScrollbar({
    suppressScrollX: true
  });
    var righttnav = $("#chat-out").height();
  $('.rightside-navigation').height(righttnav).perfectScrollbar({
    suppressScrollX: true
  }); 

  //// Floating-Fixed table of contents (Materialize pushpin)
  //if ($('nav').length) {
  //  $('.toc-wrapper').pushpin({
  //    top: $('nav').height()
  //  });
  //}
  //else if ($('#index-banner').length) {
  //  $('.toc-wrapper').pushpin({
  //    top: $('#index-banner').height()
  //  });
  //}
  //else {
  //  $('.toc-wrapper').pushpin({
  //    top: 0
  //  });
  //}

 
  
  ////Toggle Containers on page
  //var toggleContainersButton = $('#container-toggle-button');
  //toggleContainersButton.click(function() {
  //  $('body .browser-window .container, .had-container').each(function() {
  //    $(this).toggleClass('had-container');
  //    $(this).toggleClass('container');
  //    if ($(this).hasClass('container')) {
  //      toggleContainersButton.text("Turn off Containers");
  //    }
  //    else {
  //      toggleContainersButton.text("Turn on Containers");
  //    }
  //  });
  //});

  // Detect touch screen and enable scrollbar if necessary
  function is_touch_device() {
    try {
      document.createEvent("TouchEvent");
      return true;
    }
    catch (e) {
      return false;
    }
  }
  if (is_touch_device()) {
    $('#nav-mobile').css({
      overflow: 'auto'
    })
  }

}); // end of document ready