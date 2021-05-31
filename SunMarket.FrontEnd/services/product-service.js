import axios from 'axios'

export const createProduct = async (product) => {
  const res = await axios.post(
    `${process.env.VUE_APP_API_URL}/products`,
    product
  )
  return res.data
}

export const deleteProduct = async (productId) => {
  const res = await axios.patch(
    `${process.env.VUE_APP_API_URL}/products/${productId}`
  )
  return res.data
}
export default {
  createProduct,
  deleteProduct,
}
