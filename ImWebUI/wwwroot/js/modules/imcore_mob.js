
layui.define(['layer', 'mobile'], function (exports) {



    var $ = layui.$,
        mobile = layui.mobile,
        websocketId = '',
        mine = {
            "username": "未名氏" + (Math.random() * 100 | 0) //我的昵称
            , "id": websocketId //我的ID
            , "status": "online" //在线状态 online：在线、hide：隐身
            , "sign": "FreeSql是最棒的" //我的签名
            , "avatar": "4a02849cjw8fc8vn18vktj20hs0hs75v.jpg" //我的头像
        },
        socket = null,
        layim = mobile.layim,
        layer = mobile.layer;
    var data = $.ajax({ url: "/api/Authorization", type: 'get', async: false }).responseJSON;


    layer.open({
        title: '给自己取个牛逼的名字吧'
        , closeBtn: false
        , area: ['500px', '300px']
        , content: `<div id="mine">
        <form class="layui-form" action="">
            <div class="layui-form-item">
                <label class="layui-form-label">名字</label>
                <div class="layui-input-block">
                    <input type="text" name="username" required lay-verify="required" placeholder="一定要取个响铛铛的名字，比如：骑得龙东墙" autocomplete="off" class="layui-input">
                </div>
            </div>
            <div class="layui-form-item">
                <label class="layui-form-label">头像</label>
                <div class="layui-input-block">
                    <input type="text" name="avatar" required lay-verify="required" placeholder="请输入头像,你若不填别怪我无情" autocomplete="off" class="layui-input">
                </div>
            </div>
            <div class="layui-form-item">
                <label class="layui-form-label">签名</label>
                <div class="layui-input-block">
                    <input type="text" name="sign" required lay-verify="required" placeholder="FreeSql是最棒的" autocomplete="off" class="layui-input">
                </div>
            </div>
        </form>
    </div>`
        , btn: "确定"
        , yes: function (index, layero) {
            mine = {
                "username": $('input[name="username"]').val() || "未名氏" + (Math.random() * 100 | 0) //我的昵称
                , "id": websocketId //我的ID
                , "status": "online" //在线状态 online：在线、hide：隐身
                , "sign": $('input[name="sign"]').val() || "FreeSql是最棒的" //我的签名
                , "avatar": $('input[name="avatar"]').val() || "4a02849cjw8fc8vn18vktj20hs0hs75v.jpg" //我的头像
            };
            layer.closeAll();
            init();
        }
    });



    console.log(data);
    websocketId = data.websocketId;
    var socket = new WebSocket(data.server);
    socket.onmessage = function (res) {
        var data = JSON.parse(res.data);

        if (data.type === 'group' && data.sendId != websocketId) {
            layim.getMessage(data.data);
        }
    };

    //基础配置


    layim.on('ready', function (options) {
        console.log(options);

    });

    layim.on('sendMessage', function (res) {
        console.log(res);
        $.ajax({
            type: 'post',
            url: '/api/sendMsg',
            contentType: 'application/json',
            data: JSON.stringify(res),
            success: function (e) {
                console.log(e);
            }
        });
        console.log('end')
    });




    $.ajax({
        type: 'post',
        url: 'api/group',
        data: `clientId=${websocketId}&chan=fffeeeddd101`,
        success: function (e) { }
    })
    $.ajax({
        type: 'post',
        url: 'api/group',
        data: `clientId=${websocketId}&chan=fffeeeddd102`,
        success: function (e) { }
    })
    var init = function () {

        var data = {
            "mine": mine
            , "friend": [{
                "groupname": "前端码屌"
                , "id": 1
                , "online": 2
                , "list": [{
                    "username": "Z_子晴"
                    , "id": "108101"
                    , "avatar": "http://tva3.sinaimg.cn/crop.0.0.512.512.180/8693225ajw8f2rt20ptykj20e80e8weu.jpg"
                    , "sign": "微电商达人"
                }, {
                    "username": "Lemon_CC"
                    , "id": "102101"
                    , "avatar": "http://tp2.sinaimg.cn/1833062053/180/5643591594/0"
                    , "sign": ""
                }, {
                    "username": "马小云"
                    , "id": "168168"
                    , "avatar": "http://tp4.sinaimg.cn/2145291155/180/5601307179/1"
                    , "sign": "让天下没有难写的代码"
                    , "status": "offline"
                }, {
                    "username": "徐小峥"
                    , "id": "666666"
                    , "avatar": "http://tp2.sinaimg.cn/1783286485/180/5677568891/1"
                    , "sign": "代码在囧途，也要写到底"
                }]
            }, {
                "groupname": "网红"
                , "id": 2
                , "online": 3
                , "list": [{
                    "username": "罗玉凤"
                    , "id": "121286"
                    , "avatar": "http://tp1.sinaimg.cn/1241679004/180/5743814375/0"
                    , "sign": "在自己实力不济的时候，不要去相信什么媒体和记者。他们不是善良的人，有时候候他们的采访对当事人而言就是陷阱"
                }, {
                    "username": "长泽梓Azusa"
                    , "id": "100001222"
                    , "sign": "我是日本女艺人长泽あずさ"
                    , "avatar": "http://tva1.sinaimg.cn/crop.0.0.180.180.180/86b15b6cjw1e8qgp5bmzyj2050050aa8.jpg"
                }, {
                    "username": "大鱼_MsYuyu"
                    , "id": "12123454"
                    , "avatar": "http://tp1.sinaimg.cn/5286730964/50/5745125631/0"
                    , "sign": "我瘋了！這也太準了吧  超級笑點低"
                }, {
                    "username": "谢楠"
                    , "id": "10034001"
                    , "avatar": "http://tp4.sinaimg.cn/1665074831/180/5617130952/0"
                    , "sign": ""
                }, {
                    "username": "柏雪近在它香"
                    , "id": "3435343"
                    , "avatar": "http://tp2.sinaimg.cn/2518326245/180/5636099025/0"
                    , "sign": ""
                }]
            }, {
                "groupname": "我心中的女神"
                , "id": 3
                , "online": 1
                , "list": [{
                    "username": "佟丽娅"
                    , "id": "4803920"
                    , "avatar": "http://tp4.sinaimg.cn/1345566427/180/5730976522/0"
                    , "sign": "我也爱贤心吖吖啊"
                }]
            }]
            , "group": [{
                "groupname": "ImCore群"
                , "id": "fffeeeddd101"
                , "avatar": "http://tp2.sinaimg.cn/2211874245/180/40050524279/0"
            }, {
                "groupname": "FreeSql官方群"
                    , "id": "fffeeeddd102"
                , "avatar": "http://tp2.sinaimg.cn/5488749285/50/5719808192/1"
            }]
        }


        layim.config({

            init: {
                "mine": data.mine,
                "group": data.group,
                "friend": data.friend

            } //获取主面板列表信息，下文会做进一步介绍

            //获取群员接口（返回的数据格式见下文）
            , members: {
                url: '' //接口地址（返回的数据格式见下文）
                , type: 'get' //默认get，一般可不填
                , data: {} //额外参数
            }

            //上传图片接口（返回的数据格式见下文），若不开启图片上传，剔除该项即可
            , uploadImage: {
                url: '/api/UploadImage' //接口地址
                , type: 'post' //默认post
                , acceptMime: 'image/*'
                , size: 512
                , accept: 'images'
            }

            //上传文件接口（返回的数据格式见下文），若不开启文件上传，剔除该项即可
            , uploadFile: {
                url: '/api/UploadFile' //接口地址
                , type: 'post' //默认post
            }
            //扩展工具栏，下文会做进一步介绍（如果无需扩展，剔除该项即可）
            , tool: [{
                alias: 'code' //工具别名
                , title: '代码' //工具名称
                , icon: '&#xe64e;' //工具图标，参考图标文档
            }]
            , isgroup: true //是否开启“群聊”
            , msgbox: layui.cache.dir + 'css/modules/layim/html/msgbox.html' //消息盒子页面地址，若不开启，剔除该项即可
            , find: layui.cache.dir + 'css/modules/layim/html/find.html' //发现页面地址，若不开启，剔除该项即可
            , chatLog: layui.cache.dir + 'css/modules/layim/html/chatlog.html' //聊天记录页面地址，若不开启，剔除该项即可
        });


    }









    exports('imcore_mob', {});
});

