var List = {
    template: '#list-company-template',
    name: 'List',
    data: function () {
        return {
            age: 23,
            deleteCompanyId: 0,
            companies: [],
            searchKey: 0,
            editCompanyModel: {
                Name: "",
                Phone: "",
                AddressList: [
                    {
                        StreetAddress1: "",
                        StreetAddress2: "",
                        Region: "",
                        PostCode: "",
                        Country: {
                            Name: ""
                        },
                    },

                ],
            },
            addCompanyModel: {
                Name: "",
                Phone: "",
                AddressList: [
                    {
                        StreetAddress1: "",
                        StreetAddress2: "",
                        Region: "",
                        PostCode: "",
                        Country: {
                            Name: ""
                        },
                    },
                ],
            }
        }
    },

    methods: {
        displayAllCompanies: function () {
            var self = this;
            axios.get('/HomeController2/GetAllCompanies')
                .then(function (response) {
                    self.companies = response.data;
                })
        },
        editCompany: function () {
            var self = this;
            debugger;
            axios.put('/HomeController2/Update', self.editCompanyModel)
                .then(response => {
                    console.log('Company updated successfully');
                    if (response.status === 200) {
                        console.log('Company deleted successfully');
                        location.reload();
                        this.closeEditModal
                    }

                })
                .catch(error => {
                    console.error('Error updating company:', error);
                });
        },

        deleteCompany: function () {
            debugger;
            var self = this;
            axios
                .delete('/HomeController2/Delete?companyId=' + self.deleteCompanyId)
                .then(response => {
                    if (response.status === 200) {
                        console.log('Company deleted successfully');
                        location.reload();
                    }
                })
                .catch(error => {
                    console.error('Error deleting company:', error);
                });
        },
        addCompany: function () {
            var self = this;

            axios.post('/HomeController2/Create', self.addCompanyModel)
                .then(response => {
                    if (response.status === 200) {
                        console.log('Company deleted successfully');
                        location.reload();
                        this.closeAddModal();
                    }
                })
                .catch(error => {
                    console.error('Error adding company:', error);
                });
        },

        showAddModal: function () {
            $("#addCompanyModal").css("display", "block");
            $("#addCompanyModal").modal('show');
        },

        showEditModal: function (company) {

            this.editCompanyModel = company;
            $("#editCompanyModal").css("display", "block");
            $("#editCompanyModal").modal('show');
        },

        showViewModal: function (company) {
            this.editCompanyModel = company;
            $("#viewCompanyModal").css("display", "block");
            $("#viewCompanyModal").modal('show');
        },

        showDeleteModal: function (companyId) {
            this.deleteCompanyId = companyId;
            $("#deleteCompanyModal").css("display", "block");
            $("#deleteCompanyModal").modal('show');
        },

        closeAddModal: function () {
            $("#addCompanyModal").modal("hide");
        },
        closeEditModal: function () {
            $("#editCompanyModal").modal("hide");
        },
        closeDeleteModal: function () {
            $("#deleteCompanyModal").modal("hide");
        },
        closeViewModal: function () {
            $("#viewCompanyModal").modal("hide");
        },
    },

    mounted: function () {
        this.displayAllCompanies();
    }
}
