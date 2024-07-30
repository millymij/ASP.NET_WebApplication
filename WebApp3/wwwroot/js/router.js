var router = new VueRouter({
    mode: 'history',
    routes: [
        {
            path: '/',
            component: List
        },
        {
            path: '/add',
            component: Add
        },
        {
            path: '/edit',
            component: Edit
        },
    ]
});