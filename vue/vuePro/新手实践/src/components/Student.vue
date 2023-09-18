<!--template组件结构-->
<template>
   <div class="demo">
        <h2>学生姓名:{{Name}}</h2>
        <h2>学生年龄:{{age}}</h2>

    </div>
</template>
    

<script>
//组件交互相关代码(数据、方法等)
    export default {
        name:'Student',
        data()
        {
            return {
                Name:'张三',
                age:19
            }
        },
    
    }
    
</script>

<style>
/*组件的样式*/
    .demo
    {
        background-color:orange;
    }

</style>