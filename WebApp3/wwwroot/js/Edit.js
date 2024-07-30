var Edit = Vue.extend({
    template: '#editCompany',
    name: 'Edit',
    data: function () {
        return {
            company: {
            }
        };
    },
    methods: {
       editCompany: function (company) {
            var self = this;
            axios.put('/api/Company/', company).then(function (response) {
                console.log('Company created successfully:', response.data);
            })
        }
    }
});
