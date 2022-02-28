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
            <nav>
                <router-link to="/" class="delayedAnimation" @click="scrollToTop">
                    <div ref="imageElement">
                        <svg class="w-20" viewBox="0 0 100 100">
                            <use href="@/Assets/Images/RestaurantIcon.svg#restaurant" />
                        </svg>
                    </div>
                </router-link>
            </nav>
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
            <div class="flex flex-1 justify-end items-center space-x-4">
                <div class="relative flex justify-center delayedAnimation" @click.capture="showDropDown">
                    <div v-click-outside="hideDropDown">
                        <svg class="w-6" viewBox="0 0 100 100">
                            <use :href="selectedTheme.imageUrl" />
                        </svg>
                    </div>
                    <div @click="hideDropDown" v-show="showDropdown" class="absolute right-0 top-full bg-white dark:bg-dark border border-dark dark:border-white p-2">
                        <template v-for="(theme, index) in themes" :key="index">
                            <div class="flex space-x-1" @click="changeTheme(theme)">
                                <svg class="w-4" viewBox="0 0 100 100">
                                    <use :href="theme.imageUrl" />
                                </svg>
                                <button>
                                    {{ theme.name.charAt(0).toUpperCase() + theme.name.slice(1) }}
                                </button>
                            </div>
                        </template>
                    </div>
                </div>
                <nav class="hamburger-menu lg:hidden delayedAnimation" @click="hamburgerMenuOnClick">
                    <hr />
                    <hr />
                    <hr />
                </nav>
                <nav class="navigation-button delayedAnimation">
                    <div class="button" @click="scrollToTop">
                        <router-link to="/order">
                            <WaveButton
                                class="dark:border-white"
                                :background-color="isDarkMode ? 'white' : 'black'"
                                :text-color="isDarkMode ? 'white' : 'black'"
                                :text-hover-color="isDarkMode ? 'black' : 'white'"
                                buttontext="ORDER NOW"
                            />
                        </router-link>
                    </div>
                </nav>
            </div>
        </div>
    </div>
</template>

<script lang="ts" src="./Header.ts" />

<style scoped lang="scss" src="./Styles/Header.scss" />
