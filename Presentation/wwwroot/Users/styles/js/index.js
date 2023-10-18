jQuery(function ($) {
    $(document).on('click', '#addToCart', function () {

        var id = $(this).data('id');
        $.ajax({
            method: "GET",
            url: "/basket/add",
            data: {
                id: id
            },
            success: function (res) {
                console.log(res)
                alert(res)
            },
            error: function (err) {
                alert(err.responseText)
            }
        })
    })
})


//// <!-- Tab menu -->
//function openCategory(evt, categoryName) {
//      var i, tabcontent, tablinks;
//      tabcontent = document.getElementsByClassName("tabcontent");
//      for (i = 0; i < tabcontent.length; i++) {
//        tabcontent[i].style.display = "none";
//      }
//      tablinks = document.getElementsByClassName("tablinks");
//      for (i = 0; i < tablinks.length; i++) {
//          tablinks[i].className = tablinks[i].className.replace(" active-tab", "");
//          console.log(categoryName);
//      }
//    document.getElementById(categoryName).style.display = "block";
//      evt.currentTarget.className += " active-tab";
//    }

//    // Get the element with id="defaultOpen" and click on it
//    document.getElementById("defaultOpen").click();

    

      // <!-- tab-menu slider 
 jQuery("#carousel").owlCarousel({
  autoplay: false,
  rewind: true, /* use rewind if you don't want loop */
  margin: 20,
   /*
  animateOut: 'fadeOut',
  animateIn: 'fadeIn',
  */
  responsiveClass: true,
  autoHeight: true,
  autoplayTimeout: 7000,
  smartSpeed: 800,
  nav: true,
  responsive: {
    0: {
      items: 1
    },

    600: {
      items: 3
    },

    1024: {
      items: 3
    },

    1366: {
      items: 5
    }
  }
});

      //  hamburger-menu 
        function closeNav() {
          document.getElementById("mySidenav").style.width = "0";
        }
        function openNav() {
          document.getElementById("mySidenav").style.width = "350px";
        }
        
      



      // Timer top Part
        function padWithZero(number) {
          // Function to pad a number with a leading zero if it's less than 10
          return number < 10 ? `0${number}` : `${number}`;
        }
      
        var countDownDate = new Date("Oct 23, 2023 00:00:00").getTime();
        var x = setInterval(function () {
          var now = new Date().getTime();
          var distance = countDownDate - now;
      
          var days = Math.floor(distance / (1000 * 60 * 60 * 24));
          var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
          var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
          var seconds = Math.floor((distance % (1000 * 60)) / 1000);
      
          document.getElementById("days").innerHTML = padWithZero(days);
          document.getElementById("hours").innerHTML = padWithZero(hours);
          document.getElementById("minutes").innerHTML = padWithZero(minutes);
          document.getElementById("seconds").innerHTML = padWithZero(seconds);
      
          if (distance < 0) {
            clearInterval(x);
            document.getElementById("days").innerHTML = "00";
            document.getElementById("hours").innerHTML = "00";
            document.getElementById("minutes").innerHTML = "00";
            document.getElementById("seconds").innerHTML = "00";
          }
        }, 1000);


      
      // <Timer Product latest Deals 
        function padWithZero(number) {
          // Function to pad a number with a leading zero if it's less than 10
          return number < 10 ? `0${number}` : `${number}`;
        }
      
        var countDownDate = new Date("Oct 13, 2023 00:00:00").getTime();
        var x = setInterval(function () {
          var now = new Date().getTime();
          var distance = countDownDate - now;
      
          var days2 = Math.floor(distance / (1000 * 60 * 60 * 24));
          var hours2 = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
          var minutes2 = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
          var seconds2 = Math.floor((distance % (1000 * 60)) / 1000);
      
          document.getElementById("days2").innerHTML = padWithZero(days2);
          document.getElementById("hours2").innerHTML = padWithZero(hours2);
          document.getElementById("minutes2").innerHTML = padWithZero(minutes2);
          document.getElementById("seconds2").innerHTML = padWithZero(seconds2);
          //console.log(document.getElementsByClassName("seconds2").innerHTML);
      
          if (distance < 0) {
            clearInterval(x);
            document.getElementById("days2").innerHTML = "00";
            document.getElementById("hours2").innerHTML = "00";
            document.getElementById("minutes2").innerHTML = "00";
            document.getElementById("seconds2").innerHTML = "00";
          }
        }, 1000);



//Modal//////////////////////////////////////////////////////


    // Get the modal and buttons
    var modal = document.getElementById("myModal");
    var openBtn = document.getElementById("openModal");
    var closeBtn = document.getElementById("closeModal");

    // Open the modal when the "Open Modal" button is clicked
    openBtn.addEventListener("click", function () {
        modal.classList.remove("hidden")
        modal.classList.add("block")
    modal.classList.add("opacity-100")
    });

    // Close the modal when the "Close Modal" button is clicked
    closeBtn.addEventListener("click", function () {
        modal.classList.remove("block")
        modal.classList.add("hidden")

    });

    // Close the modal when the overlay is clicked
    modal.addEventListener("click", function (event) {
        if (event.target === modal) {
        modal.classList.remove("block")
            modal.classList.add("hidden")
        }
    });

