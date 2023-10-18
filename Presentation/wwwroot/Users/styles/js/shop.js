// <!-- hamburger-menu -->
    function closeNav() {
      document.getElementById("mySidenav").style.width = "0";
    }
    function openNav() {
      document.getElementById("mySidenav").style.width = "350px";
    }

//   <!-- hamburger-menu filter responsive-->
      function closeNav2() {
        document.getElementById("mySidenav2").style.width = "0px";
      }
      function openNav2() {
        document.getElementById("mySidenav2").style.width = "75%";
      }

//   <!-- Multi Range Slider -->
    function range() {
      return {
        minprice: 1000,
        maxprice: 7000,
        min: 100,
        max: 10000,
        minthumb: 0,
        maxthumb: 0,

        mintrigger() {
          this.minprice = Math.min(this.minprice, this.maxprice - 500);
          this.minthumb =
            ((this.minprice - this.min) / (this.max - this.min)) * 100;
        },

        maxtrigger() {
          this.maxprice = Math.max(this.maxprice, this.minprice + 500);
          this.maxthumb =
            100 - ((this.maxprice - this.min) / (this.max - this.min)) * 100;
        },
      };
}

