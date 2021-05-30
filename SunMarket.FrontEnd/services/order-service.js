import axios from 'axios'

export const generateNewOrder = async (invoice) => {
  const res = await axios.post(
    `${process.env.VUE_APP_API_URL}/invoice`,
    invoice
  )
  return res.data
}
export const getOrders = async () => {
  const res = await axios.get(`${process.env.VUE_APP_API_URL}/orders`)
  return res.data
}
export const markOrderComplete = async (id, isPaid) => {
  const res = await axios.patch(
    `${process.env.VUE_APP_API_URL}/order/complete/${id}/${isPaid}`
  )
  return res.data
}
export default {
  generateNewOrder,
  getOrders,
  markOrderComplete,
}
