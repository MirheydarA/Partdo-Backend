      const login = document.getElementById("login");
      const register = document.getElementById("register");
      const  loginForm = document.getElementById("loginForm");
      const registerForm = document.getElementById("registerForm");

      login.addEventListener("click" , (e) => {
        e.preventDefault()

        login.classList.add("text-black");
        register.classList.remove("text-black");
        loginForm.classList.add("block");
        loginForm.classList.remove("hidden");
        registerForm.classList.add("hidden");
        registerForm.classList.remove("block");

      })
      register.addEventListener("click" , (e) => {
        e.preventDefault()

        register.classList.add("text-black");
        login.classList.remove("text-black");
        loginForm.classList.add("hidden");
        loginForm.classList.remove("block");
        registerForm.classList.add("block");
        registerForm.classList.remove("hidden");

      })
      

