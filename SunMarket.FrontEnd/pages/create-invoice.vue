<template>
  <v-row justify="center" align="center">
    <v-col cols="12" sm="8" xl="6">
      <h1 class="mb-8">
        <v-icon color="primary" large>mdi-note-plus</v-icon>
        Create Invoice
      </h1>

      <v-stepper v-model="stepper.current">
        <v-stepper-header>
          <template v-for="(step, index) in stepper.steps">
            <v-stepper-step
              :key="`${index}-step`"
              :step="index + 1"
              :complete="stepper.current > index + 1"
            >
              {{ step.title }}
            </v-stepper-step>

            <v-divider
              v-if="index + 1 < stepper.steps.length"
              :key="index + 1"
            ></v-divider>
          </template>
        </v-stepper-header>

        <v-stepper-items>
          <v-stepper-content
            v-for="(step, index) in stepper.steps"
            :key="`${index}-content`"
            :step="index + 1"
          >
            <!-- Step Content #1 -->
            <template v-if="step.key == 'customer' && customers.length > 0">
              <v-form ref="form-invoice-customer">
                <v-select
                  v-model="invoice.customerId"
                  :items="customers"
                  item-value="id"
                  :item-text="
                    (customer) => `${customer.firstName} ${customer.lastName}`
                  "
                  label="Customer Name"
                  required
                  :rules="rules.customer"
                ></v-select>
              </v-form>
            </template>

            <!-- Step Content #2 -->
            <template
              v-if="step.key == 'products' && productInventories.length > 0"
            >
              <v-form ref="form-invoice-product">
                <v-select
                  v-model="salesOrderItem.product"
                  :items="productInventories"
                  item-value="product"
                  item-text="product.name"
                  label="Product Name"
                  required
                  :rules="rules.product"
                ></v-select>
                <v-text-field
                  v-model="salesOrderItem.quantity"
                  label="Quantity"
                  required
                  :rules="rules.quantity"
                >
                </v-text-field>
              </v-form>
              <div class="my-6 text-center">
                <v-btn color="blue darken-1" text @click="addLineProduct()">
                  Add Line Product
                </v-btn>
                <v-icon class="d-block" color="blue darken-1">
                  mdi-chevron-double-down
                </v-icon>
              </div>
              <v-simple-table v-if="invoice.salesOrderItems.length > 0">
                <template #default>
                  <thead>
                    <tr>
                      <th class="text-left">Name</th>
                      <th class="text-left">Unit Price</th>
                      <th class="text-left">Subtotal</th>
                      <th class="text-right">Quantity & Actions</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="(item, i) in invoice.salesOrderItems" :key="i">
                      <td>{{ item.product.name }}</td>
                      <td>{{ item.product.price }}</td>
                      <td>{{ item.product.price * item.quantity }}</td>
                      <td>
                        <v-row
                          justify="center"
                          align="center"
                          class="float-right"
                        >
                          <v-col justify="center" align="center">
                            <div>
                              <v-icon
                                class="my-1"
                                color="blue darken-1"
                                @click="item.quantity++"
                              >
                                mdi-chevron-up
                              </v-icon>
                            </div>

                            <div>{{ item.quantity }}</div>
                            <div>
                              <v-icon
                                class="my-1"
                                color="blue darken-1"
                                @click="decreaseProductQty(item)"
                              >
                                mdi-chevron-down
                              </v-icon>
                            </div>
                          </v-col>
                          <v-col justify="center" align="center">
                            <div>
                              <v-icon
                                color="error"
                                @click="removeLineProduct(item)"
                              >
                                mdi-trash-can-outline
                              </v-icon>
                            </div>
                          </v-col>
                        </v-row>
                      </td>
                    </tr>
                  </tbody>
                </template>
              </v-simple-table>
            </template>

            <!-- Step Content #3 -->
            <template
              v-if="step.key == 'review' && invoice.salesOrderItems.length > 0"
            >
              <div id="invoice" class="pa-8">
                <v-row>
                  <v-col cols="12" md="6">
                    <div class="font-weight-bold text-h5">Sun Market</div>
                    <div>AA Mah. BB Sk. No:22/01</div>
                    <div>City/State</div>
                    <div>TR</div>
                  </v-col>
                  <v-col cols="12" md="6" class="text-md-right">
                    <div>
                      <span class="font-weight-bold">Order Date:</span>
                      {{ $moment(new Date()).format('MMMM Do YYYY') }}
                    </div>
                    <div>
                      <span class="font-weight-bold">Customer:</span>
                      {{ `${customer.firstName} ${customer.lastName}` }}
                    </div>
                    <div>
                      <span class="font-weight-bold">Address:</span>
                      {{ customer.primaryAddress.addressLine1 }}
                    </div>
                    <div v-if="customer.primaryAddress.addressLine2">
                      {{ customer.primaryAddress.addressLine2 }}
                    </div>
                    <div>
                      {{ customer.primaryAddress.city }} /{{
                        customer.primaryAddress.state
                      }}
                      - {{ customer.primaryAddress.country }}
                    </div>
                  </v-col>
                </v-row>
                <v-simple-table
                  v-if="invoice.salesOrderItems.length > 0"
                  class="mt-3"
                >
                  <template #default>
                    <thead>
                      <tr>
                        <th class="text-left">Name</th>
                        <th class="text-left">Description</th>
                        <th class="text-left">Unit Price</th>
                        <th class="text-left">Quantity</th>
                        <th class="text-right">Subtotal</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr v-for="(item, i) in invoice.salesOrderItems" :key="i">
                        <td>{{ item.product.name }}</td>
                        <td>{{ item.product.description }}</td>
                        <td>{{ item.product.price }}</td>
                        <td>{{ item.quantity }}</td>
                        <td class="text-right">
                          {{ item.product.price * item.quantity }}
                        </td>
                      </tr>
                    </tbody>
                    <tfoot>
                      <tr class="font-weight-bold">
                        <td colspan="2"></td>
                        <td colspan="2">Balance due upon receipt</td>
                        <td class="text-right">{{ runningTotal }}</td>
                      </tr>
                    </tfoot>
                  </template>
                </v-simple-table>
              </div>
            </template>

            <div class="mt-3">
              <v-btn
                color="primary"
                :disabled="index <= 0"
                @click="prevStep(index + 1)"
              >
                Back
              </v-btn>
              <v-btn color="primary" @click="nextStep(index + 1)">
                <span v-if="index < stepper.steps.length - 1">Next</span>
                <span v-if="index == stepper.steps.length - 1"
                  >Submit & Download</span
                >
              </v-btn>
            </div>
          </v-stepper-content>
        </v-stepper-items>
      </v-stepper>
    </v-col>
  </v-row>
</template>

<script>
import JsPDF from 'jspdf'
import html2canvas from 'html2canvas'
import customerService from '../services/customer-service'
import inventoryService from '../services/inventory-service'
import orderService from '../services/order-service'

export default {
  async asyncData() {
    const customers = await customerService.fetchCustomers()
    const productInventories = await inventoryService.fetchProductInventories()
    return { customers, productInventories }
  },

  data() {
    return {
      stepper: {
        current: 1,
        steps: [
          { key: 'customer', title: 'Select Customer' },
          { key: 'products', title: 'Manage Products' },
          { key: 'review', title: 'Review Order' },
        ],
      },
      invoice: { salesOrderItems: [] },
      salesOrderItem: {},
      rules: {
        customer: [(val) => !!val || 'Customer is required'],
        product: [(val) => !!val || 'Product is required'],
        quantity: [
          (val) =>
            (!isNaN(val) && val !== '' && val > 0) ||
            'The entered value should be number and greater than 0',
          (val) =>
            val <= this.selectedOrderItem?.quantityOnHand ||
            `The quantity excesses ${this.selectedOrderItem?.product?.name} inventory. Please enter quantity less than ${this.selectedOrderItem?.quantityOnHand}`,
        ],
      },
    }
  },

  computed: {
    customer() {
      return this.customers.find(
        (customer) => customer.id === this.invoice.customerId
      )
    },
    runningTotal() {
      return this.invoice.salesOrderItems.reduce(
        (a, b) => a + b.product.price * b.quantity,
        0
      )
    },
    selectedOrderItem() {
      return this.productInventories.find(
        (inventory) => inventory.product.id === this.salesOrderItem?.product?.id
      )
    },
  },

  methods: {
    nextStep(n) {
      if (
        (n === 1 && this.$refs['form-invoice-customer'][0].validate()) ||
        (n === 2 &&
          this.$refs['form-invoice-product'][0].validate() &&
          this.invoice.salesOrderItems.length > 0)
      )
        this.stepper.current = n + 1
      if (n === 3) this.submitInvoice()
    },
    prevStep(n) {
      this.stepper.current = n - 1
    },
    addLineProduct() {
      if (!this.$refs['form-invoice-product'][0].validate()) return

      const salesOrderItem = {
        product: this.salesOrderItem.product,
        quantity: Number(this.salesOrderItem.quantity),
      }

      const existedItem = this.invoice.salesOrderItems.find(
        (item) => item.product.id === this.salesOrderItem.product.id
      )
      if (existedItem)
        existedItem.quantity = existedItem.quantity + salesOrderItem.quantity
      else {
        this.invoice.salesOrderItems.push(salesOrderItem)
      }
    },
    removeLineProduct(item) {
      this.invoice.salesOrderItems.pop(item)
    },
    decreaseProductQty(item) {
      if (item.quantity > 0) item.quantity--
      if (item.quantity === 0) this.removeLineProduct(item)
    },
    async submitInvoice() {
      await orderService.generateNewOrder(this.invoice)
      this.downloadPdf()
      this.$router.push('/orders')
    },
    downloadPdf() {
      const pdf = new JsPDF('p', 'pt', 'a4', true)
      const invoice = document.getElementById('invoice')
      const width = invoice.clientWidth
      const height = invoice.clientHeight

      html2canvas(invoice).then((canvas) => {
        const img = canvas.toDataURL('image/png')
        pdf.addImage(img, 'PNG', 20, 20, width * 0.74, height * 0.74)
        pdf.save(`invoice_${this.customer.firstName}_${this.customer.lastName}`)
      })
    },
  },
}
</script>
