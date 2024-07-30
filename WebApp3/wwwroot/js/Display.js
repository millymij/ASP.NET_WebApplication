const Display = {
    template: '#displayCompany',
    name: 'Display',
    methods: {
        async displayCompany() {
            try {
                const response = await axios.get('/api/company');
                console.log('Company displayed successfully:', response.data);
                this.clearForm();
                this.getCompanies();
            } catch (error) {
                console.error('Error displaying company:', error);
            }
        }
    }
}