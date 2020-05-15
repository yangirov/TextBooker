<template>
  <aside role="complementary" class="page-aside">
    <button class="toggle-nav" @click="toggleMenu" >
      <i class="fas fa-chevron-left"></i>
    </button>
    
    <nav role="navigation" class="menu">
      <ul class="menu__list" id="vue-counts">
        <li class="menu__item menu__item--label">
          <i class="fa fa-bars"></i>
          <span>Шаблон</span>
        </li>
        <li class="menu__item">
          <router-link :to="{ name: 'Templates' }" class="menu__link">
            <i class="fad fa-notes-medical"></i>
            <span>Шаблон</span>
          </router-link>
        </li>
      </ul>
    </nav>
  </aside>
</template>

<script>
import { mapGetters } from 'vuex'
import { routes } from '@/router';

export default {
  name: 'PageAside',
  computed: {
    ...mapGetters('appState', ['user'])
  },
  data() {
    return {
      $pageContent: null,
      nameClass: 'menu-collapse',
      isOpen: false
    }
  },
  methods: {
    toggleMenu($event, isOpen = null) {
      this.isOpen = isOpen !== null ? isOpen : !this.isOpen;
      sessionStorage.setItem(this.nameClass, this.isOpen);
      this.$pageContent.classList.toggle(this.nameClass, this.isOpen);
    },
  },

  created() {
    this.$once('hook:mounted', () => {
      const isOpen = Boolean( JSON.parse(sessionStorage.getItem(this.nameClass)) );
      this.$pageContent = document.querySelector('.page-content');
      this.toggleMenu(event, isOpen);
    });

    this.$once('hook:destroy', () => this.$pageContent = null);
  }
}
</script>

<style>
.menu__link,
.menu__link:focus,
.menu__link:hover {
  text-decoration: none;
}
.menu__link.router-link-exact-active.router-link-active {
  background-color: #50a0f2!important;
  color: #fff!important;
}

.toggle-nav:after {
  position: relative;
}
</style>
