export default {
    content: [
        "./index.html",
        "./src/**/*.{vue,js,ts,jsx,tsx}",
    ],
    darkMode: 'class',
    theme: {
        extend: {
            container: {
                center: true,
                padding: '1rem',
                screens: {
                    sm: '600px',
                    md: '728px',
                    lg: '984px',
                    xl: '1240px',
                },
            },
            colors: {
                primary: "#68a225",
                "primary-light": "#b3de81",
                "primary-dark": "#265c00",
                "pearl": "#FFF4EB",
                "warm-gray": "#2B2B2B",
                "text-dark": "#2B2B2B",
                "silver": "#F3F4F6",
                "text-light": "#BAAF84",
                accent: "#988F6C",
                error: "#c8000a",
                success: "#86ac41",
            },
            fontFamily: {
                sans: ['Montserrat', 'ui-sans-serif', 'system-ui'],
            },

        },
    },
    plugins: [],
};