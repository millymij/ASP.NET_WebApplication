var app = new Vue({
    router: router,
    el: "#vue-app",
    data: {
        companies: [],
        company: {
            name: '',
            phone: '',
            address: {
                streetAddress1: '',
                streetAddress2: '',
                region: '',
                postCode: '',
                country: {
                    name: ''
                }
            }
        }
    },
    methods: {
        clearForm: function () {
            this.company = {
                name: '',
                phone: '',
                address: {
                    streetAddress1: '',
                    streetAddress2: '',
                    region: '',
                    postCode: '',
                    country: {
                        name: ''
                    }
                }
            };
        },

        findCompany: function (CompanyId) {
            var found = this.companies.find(function (company) {
                return company.companyId == companyId;
            });
            return found;
        }
    }
});