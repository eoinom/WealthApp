
const routes = [
  {
    path: '/',
    component: () => import('layouts/MyLayout.vue'),
    children: [
      { 
        path: '', 
        component: () => import('pages/Index.vue') 
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
        path: '/liabilities', 
        component: () => import('pages/Liabilities.vue') 
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
