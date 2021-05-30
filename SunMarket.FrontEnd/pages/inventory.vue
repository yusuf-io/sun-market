<template>
  <v-row justify="center" align="center">
    <v-col cols="12" sm="8" xl="6">
      <h1 class="mb-8">
        <v-icon color="primary" large>mdi-apps</v-icon> Inventory
      </h1>

      <inventoty-chart :snapshot-timeline="snapshotTimeline" />

      <v-data-table
        :headers="headers"
        :items="productInventories"
        class="elevation-1"
      >
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
                  <span class="d-none d-sm-inline">Product</span>
                </v-btn>
              </template>
              <v-card>
                <v-card-title class="headline primary white--text">
                  New Product
                </v-card-title>

                <v-card-text>
                  <v-form ref="form-product">
                    <v-checkbox
                      v-model="product.isTaxable"
                      label="Is the product taxable?"
                    ></v-checkbox>

                    <v-text-field
                      v-model="product.name"
                      label="Name"
                      outlined
                      required
                      :rules="rules.name"
                    >
                    </v-text-field>

                    <v-textarea
                      v-model="product.description"
                      label="Description"
                      outlined
                    ></v-textarea>

                    <v-text-field
                      v-model="product.price"
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
                  <v-btn
                    color="blue darken-1"
                    text
                    @click="saveNewProduct(product)"
                  >
                    Save
                  </v-btn>
                  <v-spacer class="d-sm-none"></v-spacer>
                </v-card-actions>
              </v-card>
            </v-dialog>

            <v-dialog v-model="dialogShipment" max-width="500px">
              <template #activator="{ on, attrs }">
                <v-btn color="primary" dark v-bind="attrs" v-on="on">
                  <v-icon left> mdi-table-column-plus-after </v-icon>
                  <span class="d-none d-sm-inline">Shipment</span>
                </v-btn>
              </template>
              <v-card>
                <v-card-title class="headline primary white--text">
                  New Shipment
                </v-card-title>

                <v-card-text>
                  <v-form ref="form-shipment" class="mt-6">
                    <v-select
                      v-model="shipment.productId"
                      :items="productInventories"
                      item-value="product.id"
                      item-text="product.name"
                      label="Product"
                      outlined
                      required
                      :rules="rules.product"
                    ></v-select>
                    <v-text-field
                      v-model="shipment.adjustment"
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
                  <v-btn
                    color="blue darken-1"
                    text
                    @click="saveNewShipment(shipment)"
                  >
                    Save
                  </v-btn>
                  <v-spacer class="d-sm-none"></v-spacer>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-toolbar>
        </template>
        <template #[`item.quantityOnHand`]="{ item }">
          <span
            :class="`${
              item.quantityOnHand < item.idealQuantity
                ? 'error--text'
                : item.quantityOnHand > increasedQuantiy
                ? 'warning--text'
                : ''
            }`"
          >
            {{ item.quantityOnHand }}
          </span>
        </template>
        <template #[`item.product.isArchived`]="{ item }">
          <v-icon color="error" @click="openDeleteDialog(item.product.id)">
            mdi-trash-can-outline
          </v-icon>
          <v-dialog v-model="dialogDelete" max-width="350px">
            <v-card>
              <v-card-title class="headline primary white--text"
                >Delete Product</v-card-title
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
                <v-btn color="blue darken-1" text @click="archiveProduct()"
                  >OK</v-btn
                >
                <v-spacer class="d-sm-none"></v-spacer>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </template>
        <template #[`item.product.isTaxable`]="{ item }">
          {{ mapIsTaxable(item) }}
        </template>
      </v-data-table>
    </v-col>
  </v-row>
</template>

<script>
import inventoryService from '../services/inventory-service'
import InventotyChart from '~/components/charts/InventotyChart.vue'

export default {
  components: {
    InventotyChart,
  },
  async asyncData() {
    const snapshotTimeline = await inventoryService.getSnapshotHistory()
    return { snapshotTimeline }
  },

  data() {
    return {
      dialogProduct: false,
      dialogShipment: false,
      dialogDelete: false,
      headers: [
        { text: 'Name', sortable: false, value: 'product.name' },
        { text: 'Quantity On-hand', sortable: false, value: 'quantityOnHand' },
        {
          text: 'Taxable',
          sortable: false,
          value: 'product.isTaxable',
        },
        { text: 'Delete', sortable: false, value: 'product.isArchived' },
      ],
      rules: {
        name: [(val) => (val || '').length > 0 || 'This field is required'],
        price: [(val) => !isNaN(val) || 'The entered value should be number'],
        quantity: [
          (val) =>
            (!isNaN(val) && val > 0) ||
            'The entered value should be number and greater than 0',
        ],
        product: [(val) => !!val || 'Product is required'],
      },
      shipment: {},
      product: {},
      productInventories: [],
      increasedQuantiy: 24,
      toArchiveProduct: null,
    }
  },

  mounted() {
    this.fetchProductInventories()
  },

  methods: {
    mapIsTaxable(isTaxable) {
      return isTaxable ? 'Yes' : 'No '
    },

    openDeleteDialog(id) {
      this.toArchiveProduct = id
      this.dialogDelete = true
    },

    async fetchProductInventories() {
      const result = await this.$axios.get(
        `${process.env.VUE_APP_API_URL}/inventory`
      )
      this.dialogShipment = false
      this.productInventories = result.data
    },

    async saveNewShipment(shipment) {
      if (!this.$refs['form-shipment'].validate()) return

      const result = await this.$axios.patch(
        `${process.env.VUE_APP_API_URL}/inventory`,
        shipment
      )
      this.$refs['form-shipment'].reset()
      this.dialogShipment = false
      this.fetchProductInventories()
      this.$nuxt.refresh()
      return result.data
    },

    async saveNewProduct(product) {
      if (!this.$refs['form-product'].validate()) return

      const result = await this.$axios.post(
        `${process.env.VUE_APP_API_URL}/products`,
        product
      )
      this.$refs['form-product'].reset()
      this.dialogProduct = false
      this.fetchProductInventories()
      return result.data
    },

    async archiveProduct() {
      const result = await this.$axios.patch(
        `${process.env.VUE_APP_API_URL}/products/${this.toArchiveProduct}`
      )
      this.dialogDelete = false
      this.fetchProductInventories()
      return result.data
    },
  },
}
</script>
