var Add = {
    template: '#addCompany',
    name: 'Add',
    data: function () {
        return {
            company: {
                name: '', phone: '', address: {
                    streetAddress1: '', streetAddress2: '',
                    region: '', postCode: '', country: { name: '' }
                }
            }
        }
    },
    methods: {
        addCompany: function (company) {
            var self = this;
            axios.post('/api/Company', company)
                .then(function (response) {
                    console.log('Company created successfully:', response.data);
                })
            /*id: Math.random().toString().split('.')[1],
            name: self.company.name,
            phone: self.company.phone,
            address: {
                streetAddress1: self.company.address.streetAddress1,
                streetAddress2: self.company.address.streetAddress2,
                region: self.company.address.region,
                postCode: self.company.address.postCode,
                country: { name: self.company.address.country.name }
            });*/
        }
    }
};

/*try {
              const response = await axios.post('/api/company', this.company);
              console.log('Company created successfully:', response.data);
              this.clearForm();
              this.getCompanies();
          } catch (error) {
              console.error('Error creating company:', error);
          }*/