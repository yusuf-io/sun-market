<template>
  <v-row justify="center" align="center">
    <v-col cols="12" sm="8" xl="6">
      <h1 class="mb-8">
        <v-icon color="primary" large>mdi-file-multiple</v-icon>
        Sales Orders
      </h1>
      <v-data-table
        disable-sort
        :headers="headers"
        :items="orders"
        class="elevation-1"
      >
        <template #[`item.order`]="{ item }">
          {{ getTotal(item) }}
        </template>

        <template #[`item.isPaid`]="{ item }">
          <span :class="item.isPaid ? '' : 'error--text'">
            {{ item.isPaid ? 'Paid is full' : 'Unpaid' }}
          </span>
        </template>

        <template #[`item.complete`]="{ item }">
          <v-checkbox
            v-model="item.isPaid"
            @click="markComplete(item)"
          ></v-checkbox>
        </template>
      </v-data-table>
    </v-col>
  </v-row>
</template>

<script>
import orderService from '../services/order-service'

export default {
  async asyncData() {
    const orders = await orderService.getOrders()
    return { orders }
  },

  data() {
    return {
      headers: [
        { text: 'Customer ID', sortable: false, value: 'customer.id' },
        {
          text: 'Order ID',
          value: 'id',
        },
        {
          text: 'Order Total',
          value: 'order',
        },
        {
          text: 'Order Status',
          value: 'isPaid',
        },
        { text: 'Mark Complete', value: 'complete' },
      ],
    }
  },

  methods: {
    getTotal(order) {
      return order.salesOrderItems.reduce(
        (a, b) => a + b.product.price * b.quantity,
        0
      )
    },
    async markComplete(order) {
      await orderService.markOrderComplete(order.id, order.isPaid)
      await this.$nuxt.refresh()
    },
  },
}
</script>
