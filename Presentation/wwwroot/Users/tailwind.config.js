/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./shop.html", "./blogs.html", "./blog-single.html", "./about-us.html", "./my-account.html", "./order-tracking.html", "./wishlist.html", "./cart.html", "./contact.html", "./index.html", "./product.html", "./notfound.html"],
    theme: {

        container: {
            margin: {
                // "mobile": "30px",
                // "msm": "0"
                DEFAULT: '1rem',
                sm: '2rem',
                lg: '4rem',
                xl: '5rem',
                '2xl': '6rem',
            }
        },

        extend: {
            screens: {
                'mobile': '350px',

                'sm': '640px',
                // => @media (min-width: 640px) { ... }

                'md': '768px',
                // => @media (min-width: 768px) { ... }

                'mid': '992px',

                'lg': '1024px',
                // => @media (min-width: 1024px) { ... }

                'xl': '1200px',
                // => @media (min-width: 1200px) { ... }

                '2xl': '1636px',
                // => @media (min-width: 1536px) { ... }


                'm2xl': { 'max': '1535px' },
                // => @media (max-width: 1535px) { ... }

                'mxl': { 'max': '1199' },
                // => @media (max-width: 1279px) { ... }

                'mlg': { 'max': '1023px' },
                // => @media (max-width: 1023px) { ... }

                'mmid': { 'max': '992' },

                'mmd': { 'max': '767px' },
                // => @media (max-width: 767px) { ... }

                'msm': { 'max': '639px' },
                // => @media (max-width: 639px) { ... }


                'mid-xl': { 'min': '992px', 'max': '1200px' }

            },

            colors: {
                'red': 'red',
                'gray': '#7e7e7e',
                'border': '#e1e1e1',
                'partdored': '#ff3a4a'
            },

            keyframes: {
                colorr: {
                    '0%, 100%': { bg: '0% 50%' },
                    '50%': { bg: '100% 50%' },
                }
            },

            animation: {
                colorr: 'colorr 12s ease-in-out infinite'
            },

        },
    },
    plugins: [],
}

