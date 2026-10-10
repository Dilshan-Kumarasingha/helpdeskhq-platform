import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      // The API runs on 8080 while I develop. The code only uses /api/... paths.
      '/api': 'http://localhost:8080'
    }
  }
})