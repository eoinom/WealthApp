
const routes = [ 
  {
    path: '/login',
    component: () => import('pages/Login.vue'),
  },
  { 
    path: '/', 
    redirect: '/login' 
  },
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      // { 
      //   path: '', 
      //   component: () => import('pages/Index.vue') 
      // },
      { 
        path: '/dashboard', 
        component: () => import('pages/Dashboard.vue') 
      },
      { 
        path: '/accounts', 
        component: () => import('pages/Accounts.vue') 
      },
      { 
        path: '/crypto', 
        component: () => import('pages/Crypto.vue') 
      },
      { 
        path: '/properties', 
        component: () => import('pages/Properties.vue') 
      },
      { 
        path: '/loans', 
        component: () => import('pages/Loans.vue') 
      }
    ]
  }
]

// Always leave this as last one
if (process.env.MODE !== 'ssr') {
  routes.push({
    path: '*',
    component: () => import('pages/Error404.vue')
  })
}

export default routes
