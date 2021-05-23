<template>
  <v-row justify="center" align="center">
    <v-col cols="12" sm="8" xl="6">
      <h1 class="mb-8">
        <v-icon color="primary" large>mdi-apps</v-icon> Inventory
      </h1>
      <v-data-table :headers="headers" class="elevation-1">
        <template #top>
          <v-toolbar flat>
            <v-toolbar-title>Actions</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-dialog v-model="dialogProduct" max-width="500px">
              <template #activator="{ on, attrs }">
                <v-btn
                  color="primary"
                  dark
                  v-bind="attrs"
                  class="mr-2"
                  v-on="on"
                >
                  <v-icon left> mdi-shape-plus </v-icon>
                  Product
                </v-btn>
              </template>
              <v-card>
                <v-card-title class="headline primary white--text">
                  New Product
                </v-card-title>

                <v-card-text>
                  <v-form>
                    <v-checkbox label="Is the product taxable?"></v-checkbox>

                    <v-text-field
                      label="Name"
                      outlined
                      required
                      :rules="rules.name"
                    >
                    </v-text-field>

                    <v-textarea label="Description" outlined></v-textarea>

                    <v-text-field
                      label="Price"
                      outlined
                      required
                      :rules="rules.price"
                    >
                    </v-text-field>
                  </v-form>
                </v-card-text>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="blue darken-1"
                    text
                    @click="dialogProduct = false"
                  >
                    Cancel
                  </v-btn>
                  <v-btn color="blue darken-1" text> Save </v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>

            <v-dialog v-model="dialogShipment" max-width="500px">
              <template #activator="{ on, attrs }">
                <v-btn color="primary" dark v-bind="attrs" v-on="on">
                  <v-icon left> mdi-table-column-plus-after </v-icon>
                  Shipment
                </v-btn>
              </template>
              <v-card>
                <v-card-title class="headline primary white--text">
                  New Shipment
                </v-card-title>

                <v-card-text>
                  <v-form class="mt-6">
                    <v-select
                      :items="products"
                      label="Product"
                      outlined
                    ></v-select>

                    <v-text-field
                      label="Quantity"
                      outlined
                      required
                      :rules="rules.quantity"
                    >
                    </v-text-field>
                  </v-form>
                </v-card-text>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="blue darken-1"
                    text
                    @click="dialogShipment = false"
                  >
                    Cancel
                  </v-btn>
                  <v-btn color="blue darken-1" text> Save </v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
            <v-dialog v-model="dialogDelete" max-width="500px">
              <v-card>
                <v-card-title class="headline"
                  >Are you sure you want to delete this item?</v-card-title
                >
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" text>Cancel</v-btn>
                  <v-btn color="blue darken-1" text>OK</v-btn>
                  <v-spacer></v-spacer>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-toolbar>
        </template>
      </v-data-table>
    </v-col>
  </v-row>
</template>

<script>
export default {
  data() {
    return {
      dialogProduct: false,
      dialogShipment: false,
      dialogDelete: false,
      headers: [
        { text: 'Name', sortable: false },
        { text: 'Quantity On-hand', sortable: false },
        { text: 'Taxable', sortable: false },
        { text: 'Delete', sortable: false },
      ],
      rules: {
        name: [(val) => (val || '').length > 0 || 'This field is required'],
        price: [(val) => !isNaN(val) || 'The entered value should be number'],
        quantity: [
          (val) => !isNaN(val) || 'The entered value should be number',
        ],
      },
      products: ['Foo', 'Bar', 'Fizz', 'Buzz'],
    }
  },
}
</script>
