/* eslint-disable */
const colors = require("tailwindcss/colors");

const flattenColorPalette = require('tailwindcss/lib/util/flattenColorPalette').default;

module.exports = {
    theme: {
        extend: {
            colors: {
                ...colors,
                gold: "#d6a159",
                darkGold: "#785a31"
            },
            scale: {
                '-100' : '-1'
            },
            height: {
                '70vh' : '70vh'
            },
            transitionDuration: {
                400: "400ms",
            },
            cursor: {
                grab: "grab",
                grabbing: "grabbing"
            },
            animation: {
                "wave-button-animation": " wave-button-animation .4s forwards",
                "wave-button-animation-reverse": "wave-button-animation-reverse .4s",
            },
            keyframes: {
                "wave-button-animation": {
                    "0%": {
                        transform: "translateY(0%)"
                    },
                    "50%": {
                        transform: "translateY(-100%)"
                    },
                    "50.1%": {
                        transform: "translateY(100%)"
                    },
                    "100%": {
                        transform: "translateY(0%)"
                    }
                },
                "wave-button-animation-reverse": {
                    "0%": {
                        transform: "translateY(0%)"
                    },
                    "50%": {
                        transform: "translateY(100%)"
                    },
                    "50.1%": {
                        transform: "translateY(-100%)"
                    },
                    "100%": {
                        transform: "translateY(0%)"
                    }
                }
            }
        },
        fontFamily: {
            sans: ["Segoe UI"]
        }
    },
    purge: ["./public/**/*.html", "./src/**/*.{vue,ts}"],
    variants: {
        extend: {
            width: ['focus'],
            backgroundColor: ['active'],
            input: ['checked'],
            borderWidth: ['last']
        }
    }
};
