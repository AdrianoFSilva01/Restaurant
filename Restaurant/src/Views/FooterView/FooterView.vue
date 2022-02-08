<template>
    <div class="footer">
        <div class="flex justify-between">
            <p class="text-2xl w-1/2">
                Stay up to date.
            </p>
            <div class="flex items-center">
                <AnimatedArrows class="w-8 h-6" :child-class="'bg-white'" @click="scrollToTop" :direction="ArrowDirection.top" />
            </div>
        </div>
        <div class="space-y-6 md:flex">
            <form class="form">
                <div class="name-inputs">
                    <input class="input" placeholder="First Name" />
                    <input class="input" placeholder="Last Name" />
                </div>
                <input class="input" placeholder="Email Adress" />
                <label class="privacy-policy">
                    <input class="input" type="checkbox" />
                    <p class="text-sm">
                        By submitting this form, you accept our privacy policy.
                    </p>
                </label>
                <WaveButton class="button" background-color="white" text-color="white" text-hover-color="#d6a159" buttontext="Send" />
            </form>
            <div class="contacts">
                <ul class="contact-list">
                    <li class="contact-item">
                        <svg class="w-5" viewBox="0 0 100 100">
                            <use href="@/Assets/Images/Facebook.svg#facebook" />
                        </svg>
                        <p>
                            Facebook
                        </p>
                    </li>
                    <li class="contact-item">
                        <svg class="w-5" viewBox="0 0 100 100">
                            <use href="@/Assets/Images/Instagram.svg#instagram" />
                        </svg>
                        <p>
                            Instagram
                        </p>
                    </li>
                    <li class="contact-item">
                        <svg class="w-5" viewBox="0 0 100 100">
                            <use href="@/Assets/Images/Phone.svg#phone" />
                        </svg>
                        <p>
                            931 662 001
                        </p>
                    </li>
                    <li class="contact-item">
                        <svg class="w-5" viewBox="0 0 100 100">
                            <use href="@/Assets/Images/Mail.svg#mail" />
                        </svg>
                        <p>
                            info@madeira.pt
                        </p>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script lang="ts" src="./FooterView.ts" />

<style scoped lang="scss" src="./Styles/FooterView.scss" />
