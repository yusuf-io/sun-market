import axios from 'axios'

export const generateNewOrder = async (invoice) => {
  const res = await axios.post(
    `${process.env.VUE_APP_API_URL}/invoice`,
    invoice
  )
  return res.data
}
export default {
  generateNewOrder,
}
