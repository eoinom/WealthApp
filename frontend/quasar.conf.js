// Configuration for your app

module.exports = function (ctx) {
  return {
    // app boot file (/src/boot)
    // --> boot files are part of "main.js"
    boot: [
      'axios',
      'global-mixins'
    ],

    css: [
      'app.styl'
    ],

    extras: [
      'roboto-font',
      'material-icons', // optional, you are not bound to it
      // 'ionicons-v4',
      // 'mdi-v3',
      'fontawesome-v5',
      // 'eva-icons'
    ],

    framework: {
      // all: true, // --- includes everything; for dev only!

      components: [
        'QLayout',
        'QHeader',
        'QDrawer',
        'QPageContainer',
        'QPage',
        'QToolbar',
        'QToolbarTitle',
        'QBtn',
        'QIcon',
        'QList',
        'QItem',
        'QItemSection',
        'QItemLabel',

        'QAvatar',
        'QFooter',
        'QTabs',
        'QTab',        
        'QRouteTab',
        'QTabPanels',
        'QTabPanel',
        'QImg',
        'QScrollArea',
        'QCard',
        'QCardSection',
        'QCardActions',
        'QTable',
        'QTh',
        'QTr',
        'QTd',
        'QTooltip',
        'QInput',
        'QCheckbox',
        'QSpace',
        'QMenu',
        'QToggle',
        'QSeparator',
        'QDate',
        'QDialog',
        'QPopupProxy',
        'QPopupEdit',
        'QTime',
        'QSelect',
        'QSlider',
        'QBadge',
        'QRadio'
      ],

      directives: [
        'Ripple',
        'ClosePopup',
        'GoBack'
      ],

      // Quasar plugins
      plugins: [
        'Dialog',
        'Loading',
        'Notify',
      ],

      config: {
        notify: { /* Notify defaults */ },
        loading: { /* Loading defaults */ }
      }

      // iconSet: 'ionicons-v4'
      // lang: 'de' // Quasar language
    },

    supportIE: false,

    build: {
      scopeHoisting: true,
      vueRouterMode: 'history',
      // vueCompiler: true,
      // gzip: true,
      // analyze: true,
      // extractCSS: false,
      extendWebpack (cfg) {
      }
    },

    devServer: {
      // https: true,
      // port: 8080,
      open: true, // opens browser window automatically
      proxy: {
        // proxy all requests starting with /graphql to jsonplaceholder
        '/graphql': {
          target: 'http://localhost:51980/graphql',
          changeOrigin: true,
          pathRewrite: {
            '^/graphql': ''
          }
        }
      }
    },

    // animations: 'all', // --- includes all animations
    animations: [],

    ssr: {
      pwa: false
    },

    pwa: {
      // workboxPluginMode: 'InjectManifest',
      // workboxOptions: {}, // only for NON InjectManifest
      manifest: {
        // name: 'Quasar App',
        // short_name: 'Quasar-PWA',
        // description: 'Best PWA App in town!',
        display: 'standalone',
        orientation: 'portrait',
        background_color: '#ffffff',
        theme_color: '#027be3',
        icons: [
          {
            'src': 'statics/icons/icon-128x128.png',
            'sizes': '128x128',
            'type': 'image/png'
          },
          {
            'src': 'statics/icons/icon-192x192.png',
            'sizes': '192x192',
            'type': 'image/png'
          },
          {
            'src': 'statics/icons/icon-256x256.png',
            'sizes': '256x256',
            'type': 'image/png'
          },
          {
            'src': 'statics/icons/icon-384x384.png',
            'sizes': '384x384',
            'type': 'image/png'
          },
          {
            'src': 'statics/icons/icon-512x512.png',
            'sizes': '512x512',
            'type': 'image/png'
          }
        ]
      }
    },

    cordova: {
      // id: 'org.cordova.quasar.app'
      // noIosLegacyBuildFlag: true // uncomment only if you know what you are doing
    },

    electron: {
      // bundler: 'builder', // or 'packager'
      bundler: 'builder',

      extendWebpack (cfg) {
        // do something with Electron main process Webpack cfg
        // chainWebpack also available besides this extendWebpack
      },

      packager: {
        // https://github.com/electron-userland/electron-packager/blob/master/docs/api.md#options

        // OS X / Mac App Store
        // appBundleId: '',
        // appCategoryType: '',
        // osxSign: '',
        // protocol: 'myapp://path',

        // Window only
        win32metadata: { 
          CompanyName: 'Wealth Industries',
          ProductName: 'wealthApp',
          InternalName: 'wealthApp',
        }
      },

      builder: {
        // https://www.electron.build/configuration/configuration

        appId: 'wealthApp'
      }
    }
  }
}
