import Vue from 'vue'
import App from './App.vue'

Vue.config.productionTip=false

new Vue({
    el:"#app",
    // template:'<App></App>',
    // components:{App},
    render:h=>h(App),
})