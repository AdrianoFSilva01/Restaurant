/* eslint-disable */
const colors = require("tailwindcss/colors");

const flattenColorPalette = require('tailwindcss/lib/util/flattenColorPalette').default;

module.exports = {
    theme: {
        extend: {
            colors: {
                ...colors,
                gold: "#d6a159"
            },
            scale: {
                '-100' : '-1'
            },
            height: {
                '70vh' : '70vh'
            }
        },
        fontFamily: {
            sans: ["Segoe UI"]
        },

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
