/*global
    piranha
*/

Vue.component("embedded-video-block", {
    props: ["uid", "toolbar", "model"],
    methods: {
        onBlur: function (e) {
            this.model.source.value = e.target.innerText;
        }
    },
    computed: {
        isEmpty: function () {
            return piranha.utils.isEmptyText(this.model.source.value);
        }
    },
    template:
           "<div class='block-body' :class='{ empty: isEmpty }'>" +
           "<div class='row'>" +
            "<div class='col-md-6 col-sm-12'>" +             
                    "<i class='fas fa-video'></i>" +
                    "<p class='lead' contenteditable='true' spellcheck='false' v-html='model.source.value' v-on:blur='onBlur'></p>" +
            "</div>" +
            "<div class='col-md-6 col-sm-12'>"+
                "<div class='embed-responsive embed-responsive-16by9'>" + 
                    "<iframe class='embed-responsive-item' :src='model.source.value' width='672' height='416' frameborder='0' allowtransparency='true' allow='encrypted-media'></iframe>" +
                "</div>" +
            "</div>" +
            "</div>" +
            "</div>" 
});