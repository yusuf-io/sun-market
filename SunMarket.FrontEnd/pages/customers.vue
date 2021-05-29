<template>
  <v-row justify="center" align="center">
    <v-col cols="12" sm="8" xl="6">
      <h1 class="mb-8">
        <v-icon color="primary" large>mdi-account-box-multiple</v-icon>
        Customers
      </h1>
      <v-data-table :headers="headers" :items="customers" class="elevation-1">
        <template #top>
          <v-toolbar flat>
            <v-toolbar-title>Actions</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-dialog v-model="dialogCustomer" max-width="500px">
              <template #activator="{ on, attrs }">
                <v-btn
                  color="primary"
                  dark
                  v-bind="attrs"
                  class="mr-2"
                  v-on="on"
                >
                  <v-icon left> mdi-account-plus </v-icon>
                  <span class="d-none d-sm-inline">Customer</span>
                </v-btn>
              </template>
              <v-card>
                <v-card-title class="headline primary white--text">
                  New Customer
                </v-card-title>

                <v-card-text>
                  <v-form ref="form-customer" class="mt-10">
                    <v-row>
                      <v-col cols="12" lg="6" class="py-0">
                        <v-text-field
                          v-model="customer.firstName"
                          label="First Name"
                          outlined
                          required
                          :rules="rules.name"
                        >
                        </v-text-field>
                      </v-col>

                      <v-col cols="12" lg="6" class="py-0">
                        <v-text-field
                          v-model="customer.lastName"
                          label="Last Name"
                          outlined
                          required
                          :rules="rules.name"
                        >
                        </v-text-field>
                      </v-col>

                      <v-col cols="12">
                        <v-text-field
                          v-model="customer.primaryAddress.addressLine1"
                          label="Address Line 1"
                          outlined
                        >
                        </v-text-field>
                      </v-col>

                      <v-col cols="12">
                        <v-text-field
                          v-model="customer.primaryAddress.addressLine2"
                          label="Address Line 2"
                          outlined
                        >
                        </v-text-field>
                      </v-col>

                      <v-col cols="12" lg="6" class="py-0">
                        <v-text-field
                          v-model="customer.primaryAddress.city"
                          label="City"
                          outlined
                        >
                        </v-text-field>
                      </v-col>

                      <v-col cols="12" lg="6" class="py-0">
                        <v-text-field
                          v-model="customer.primaryAddress.state"
                          label="State"
                          outlined
                        >
                        </v-text-field>
                      </v-col>

                      <v-col cols="12" lg="6" class="py-0">
                        <v-text-field
                          v-model="customer.primaryAddress.postalCode"
                          label="Postal Code"
                          outlined
                        >
                        </v-text-field>
                      </v-col>

                      <v-col cols="12" lg="6" class="py-0">
                        <v-text-field
                          v-model="customer.primaryAddress.country"
                          label="Country"
                          outlined
                        >
                        </v-text-field>
                      </v-col>
                    </v-row>
                  </v-form>
                </v-card-text>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="blue darken-1"
                    text
                    @click="dialogCustomer = false"
                  >
                    Cancel
                  </v-btn>
                  <v-btn
                    color="blue darken-1"
                    text
                    @click="saveNewCustomer(customer)"
                  >
                    Save
                  </v-btn>
                  <v-spacer class="d-sm-none"></v-spacer>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-toolbar>
        </template>

        <template #[`item.delete`]="{ item }">
          <v-icon color="error" @click="openDeleteDialog(item.id)">
            mdi-trash-can-outline
          </v-icon>
          <v-dialog v-model="dialogDelete" max-width="350px">
            <v-card>
              <v-card-title class="headline primary white--text"
                >Delete Customer</v-card-title
              >
              <v-card-text class="text-center">
                <v-icon class="my-8" color="error" x-large>
                  mdi-alert-circle-outline
                </v-icon>
                <h2>Are you sure?</h2>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="dialogDelete = false"
                  >Cancel</v-btn
                >
                <v-btn color="blue darken-1" text @click="deleteCustomer()"
                  >OK</v-btn
                >
                <v-spacer class="d-sm-none"></v-spacer>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </template>
        <template #[`item.name`]="{ item }">
          {{ item.firstName + ' ' + item.lastName }}
        </template>
        <template #[`item.address`]="{ item }">
          {{ item.primaryAddress.addressLine1 || '' }}
          {{ item.primaryAddress.addressLine2 || '' }}
        </template>
        <template #[`item.createOn`]="{ item }">
          <div>
            {{ $moment(item.createOn).format('MMMM Do YYYY') }}
          </div>
          <div class="grey--text">
            {{ $moment.utc(item.createOn).fromNow() }}
          </div>
        </template>
      </v-data-table>
    </v-col>
  </v-row>
</template>

<script>
export default {
  data() {
    return {
      dialogCustomer: false,
      dialogDelete: false,
      headers: [
        { text: 'Customer', sortable: false, value: 'name' },
        {
          text: 'Address',
          sortable: false,
          value: 'address',
        },
        {
          text: 'Country',
          sortable: false,
          value: 'primaryAddress.country',
        },
        {
          text: 'Since',
          sortable: false,
          value: 'createOn',
        },
        { text: 'Delete', sortable: false, value: 'delete' },
      ],
      rules: {
        name: [(val) => (val || '').length > 0 || 'This field is required'],
      },
      customer: { primaryAddress: {} },
      customers: [],
      customerId: null,
    }
  },

  mounted() {
    this.fetchCustomers()
  },

  methods: {
    openDeleteDialog(id) {
      this.customerId = id
      this.dialogDelete = true
    },

    async fetchCustomers() {
      const result = await this.$axios.get(
        `${process.env.VUE_APP_API_URL}/customers`
      )
      this.customers = result.data
    },

    async saveNewCustomer(customer) {
      if (!this.$refs['form-customer'].validate()) return

      const result = await this.$axios.post(
        `${process.env.VUE_APP_API_URL}/customer`,
        customer
      )
      this.$refs['form-customer'].reset()
      this.dialogCustomer = false
      this.fetchCustomers()
      return result.data
    },

    async deleteCustomer() {
      const result = await this.$axios.delete(
        `${process.env.VUE_APP_API_URL}/customers/${this.customerId}`
      )
      this.dialogDelete = false
      this.fetchCustomers()
      return result.data
    },
  },
}
</script>
