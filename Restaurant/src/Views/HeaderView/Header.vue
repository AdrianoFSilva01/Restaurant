<template>
    <div class="sticky top-0 z-40">
        <div ref="headerContainer" class="header-container" v-delayed-childs="[0.1, 'delayedAnimation', 'transform-opacity-animation', true]">
            <nav class="navigation-text delayedAnimation">
                <router-link to="/order" @click="scrollToTop">
                    Order
                    <br />
                    Now
                </router-link>
            </nav>
            <router-link to="/" class="delayedAnimation" @click="scrollToTop">
                <div ref="imageElement" class="navigation-image">
                    <img src="@/Assets/Images/RestaurantIcon.svg" />
                </div>
            </router-link>
            <nav class="navigation-icon delayedAnimation">
                <div class="hamburger-menu" @click="hamburgerMenuOnClick">
                    <hr />
                    <hr />
                    <hr />
                </div>
                <a class="navigation-pages">
                    <router-link to="/order" @click="scrollToTop">
                        Order
                    </router-link>
                </a>
            </nav>
            <div class="navigation-button delayedAnimation">
                <div class="button" @click="scrollToTop">
                    <router-link to="/order">
                        <WaveButton buttontext="ORDER NOW" />
                    </router-link>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts" src="./Header.ts" />

<style scoped lang="scss" src="./Styles/Header.scss" />
