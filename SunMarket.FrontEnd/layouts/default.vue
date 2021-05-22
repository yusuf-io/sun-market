<template>
  <v-app dark>
    <v-navigation-drawer v-model="drawer" :mini-variant="miniVariant" fixed app>
      <v-list-item v-if="!miniVariant">
        <v-list-item-content>
          <v-list-item-title class="title text-center">
            Dashboard
          </v-list-item-title>
          <v-list-item-subtitle class="text-center">
            Manage Your Business
          </v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-divider></v-divider>
      <v-list>
        <v-list-item
          v-for="(item, i) in items"
          :key="i"
          :to="item.to"
          router
          exact
        >
          <v-list-item-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title v-text="item.title" />
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar fixed app>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
      <v-btn icon @click.stop="miniVariant = !miniVariant">
        <v-icon>mdi-{{ `chevron-${miniVariant ? 'right' : 'left'}` }}</v-icon>
      </v-btn>
      <v-toolbar-title v-text="title" />
      <v-spacer />
      <v-btn class="mr-1" icon small @click.prevent="isDark = !isDark">
        <v-icon small
          >mdi-{{
            `${isDark ? 'white-balance-sunny' : 'moon-waxing-crescent'}`
          }}</v-icon
        >
      </v-btn>
    </v-app-bar>
    <v-main>
      <v-container>
        <nuxt />
      </v-container>
    </v-main>
    <v-footer absolute app>
      <span>&copy; {{ new Date().getFullYear() }}</span>
    </v-footer>
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      drawer: false,
      items: [
        {
          icon: 'mdi-apps',
          title: 'Welcome',
          to: '/',
        },
        {
          icon: 'mdi-chart-bubble',
          title: 'Inspire',
          to: '/inspire',
        },
      ],
      miniVariant: false,
      title: 'Sun Market',
      isDark: true,
    }
  },
  watch: {
    isDark(newValue) {
      this.$vuetify.theme.isDark = newValue
      localStorage.setItem('dark-theme', newValue)
    },
  },
  created() {
    this.isDark = JSON.parse(localStorage.getItem('dark-theme'))
  },
}
</script>
