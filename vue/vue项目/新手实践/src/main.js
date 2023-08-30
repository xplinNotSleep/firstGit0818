//import Vue from 'vue/dist/vue'//完整的vue
import Vue from 'vue'
import App from './App.vue'

Vue.config.productionTip=false

new Vue({
    el:"#app",
    //写法一：需要调用完整的vue
    // template:'<App></App>',
    // components:{App},
    //写法二:
    render:h=>h(App),
    // render(createElement)
    // {
    //     return createElement('h1','你好啊')
    // }
})