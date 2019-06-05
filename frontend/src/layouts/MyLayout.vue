<template>
  <q-layout view="hHh lpR fFf">
    <q-header>
      <q-toolbar>
        <q-btn flat dense round
          @click="leftDrawerOpen = !leftDrawerOpen"
          aria-label="Menu" 
          class="menuBtn">
          <q-icon name="menu" />
        </q-btn>

        <q-toolbar-title>
          <img alt="WealthApp Logo" src="~assets/Logo.png" style="height: 30px" class="vertical-middle">
          <!-- WealthApp.io -->
        </q-toolbar-title>

        <div class="q-gutter-md">
          <q-icon name="notifications" style="font-size: 2em;"/>
          <q-icon name="settings" style="font-size: 2em;"/>
          <q-avatar>
            <img src="https://api.adorable.io/avatars/70/abott@adorable.png">
          </q-avatar>
        </div>
      </q-toolbar>
    </q-header>

    <q-drawer
      v-model="leftDrawerOpen"
      :breakpoint="767"
      :width="220"      
      bordered
      content-class="bg-primary"
    >
      <q-list dark>
        <q-item-label header>Navigation</q-item-label>

        <q-item 
          v-for="nav in navs"
          :key="nav.label"
          :to="nav.to"
          class="text-grey-4"
          exact
          clickable>
          <q-item-section avatar>
            <q-icon :name="nav.icon" />
          </q-item-section>
          <q-item-section>
            <q-item-label>{{ nav.label }}</q-item-label>
          </q-item-section>
        </q-item>

      </q-list>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>

    <q-footer class="bg-secondary text-white">
      <q-tabs>
        <q-route-tab 
          v-for="nav in navs"
          :key="nav.label"
          :to="nav.to"
          :icon="nav.icon" 
          :label="nav.label" />
      </q-tabs>
    </q-footer>

  </q-layout>
</template>

<script>
import { openURL } from 'quasar'

export default {
  name: 'MyLayout',
  data () {
    return {
      leftDrawerOpen: this.$q.platform.is.desktop,
      navs: [
        {
          label: 'Dashboard',
          icon: 'fas fa-chart-bar',
          to: '/'
        },
        {
          label: 'Accounts',
          icon: 'account_balance_wallet',
          to: '/accounts'
        },
        {
          label: 'Crypto',
          icon: 'fab fa-bitcoin',
          to: '/crypto'
        },
        {
          label: 'Property',
          icon: 'home',
          to: '/properties'
        },
        {
          label: 'Liabilities',
          icon: 'credit_card',
          to: '/liabilities'
        }
      ]
    }
  },
  methods: {
    openURL
  }
}
</script>

<style lang="scss">
  @media screen and (min-width: 768px) {
    .q-footer {
      display: none;
    }
  }

  @media screen and (max-width: 767px) {
    .menuBtn {
      display: none;
    }
  }

  .q-drawer {
    .q-router-link--exact-active {
      color: white !important;
    }
  }
</style>
