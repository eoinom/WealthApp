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
          <!-- <q-icon name="notifications" style="font-size: 2em;"/> -->
          <!-- <q-icon name="settings" style="font-size: 2em;"/> -->
          <q-avatar>
            <img src="https://cdn.quasar.dev/img/avatar1.jpg">
            <q-menu>
              <div class="row no-wrap q-pa-md">
                <!-- <div class="column">
                  <div class="text-h6 q-mb-md">Settings</div>
                  <q-toggle v-model="mobileData" label="Use Mobile Data" />
                  <q-toggle v-model="bluetooth" label="Bluetooth" />
                </div>

                <q-separator vertical inset class="q-mx-lg" /> -->

                <div class="column items-center">
                  <q-avatar size="72px">
                    <img src="https://cdn.quasar.dev/img/avatar1.jpg">
                  </q-avatar>

                  <div class="text-subtitle1 text-center q-mt-sm">{{ userFullName }}</div>
                  <div class="text-subtitle2 text-center q-mb-sm">{{ userEmail }}</div>

                  <q-btn
                    v-go-back=" '/login' "
                    @click="logout"
                    color="primary"
                    label="Logout"
                    size="sm"
                    v-close-popup
                  />
                </div>
              </div>
            </q-menu>
          </q-avatar>
        </div>
      </q-toolbar>
    </q-header>

    <q-drawer
      v-model="leftDrawerOpen"
      :breakpoint="767"
      :width="220"  
      :overlay="addDrawerOverlay"
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
      <q-tabs >
        <!-- <q-route-tab 
          v-for="nav in navs"
          :key="nav.label"
          :to="nav.to"
          :icon="nav.icon" 
          :label="nav.label" 
        /> -->
        <q-route-tab 
          v-for="nav in navs"
          :key="nav.label"
          :to="nav.to"
          :icon="nav.icon" 
        />
      </q-tabs>
    </q-footer>

  </q-layout>
</template>

<script>
  import { openURL } from 'quasar'
  import { mapActions } from 'vuex'
  import { mapGetters } from 'vuex'

  export default {
    name: 'MyLayout',
    data () {
      return {
        leftDrawerOpen: this.$q.platform.is.desktop,
        navs: [
          {
            label: 'Dashboard',
            icon: 'fas fa-chart-bar',
            to: '/dashboard'
          },
          {
            label: 'Accounts',
            icon: 'account_balance_wallet',
            to: '/accounts'
          },
          // {
          //   label: 'Crypto',
          //   icon: 'fab fa-bitcoin',
          //   to: '/crypto'
          // },
          // {
          //   label: 'Property',
          //   icon: 'home',
          //   to: '/properties'
          // },
          // {
          //   label: 'Credit Cards',
          //   icon: 'credit_card',
          //   to: '/loans'
          // },
          {
            label: 'Loans',
            icon: 'fas fa-hand-holding-usd',
            to: '/loans'
          }
        ],
        window: {
          width: 0,
          height: 0
        }
      }
    },
    computed: {      
      ...mapGetters('main', ['userFullName', 'userEmail']),

      addDrawerOverlay() {
        if (this.window.width < 1440)
          return true
        else
          return false
      }
    },
    methods: {
      ...mapActions('main', ['logout']),
      openURL,
      handleResize: function() {
        this.window.width = window.innerWidth;
        this.window.height = window.innerHeight;
        if (this.window.width < 1440) {
          this.leftDrawerOpen = false
        }
      }
    },

    created() {
      window.addEventListener('resize', this.handleResize)
      this.handleResize();
    },

    destroyed() {
      window.removeEventListener('resize', this.handleResize)
    },
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
