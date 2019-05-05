using System.Collections.Generic;
using UnityEngine;

namespace MingUnity.FaceRec
{
    /// <summary>
    /// 人脸检测结果
    /// </summary>
    public class FaceDetectRes : FaceResBase
    {
        /// <summary>
        /// 结果
        /// </summary>
        public Result result;

        /// <summary>
        /// 结果
        /// </summary>
        public struct Result
        {
            /// <summary>
            /// 人脸数量
            /// </summary>
            public int face_num;

            /// <summary>
            /// 人脸数据列表
            /// </summary>
            public List<Face> face_list;

            /// <summary>
            /// 人脸数据
            /// </summary>
            public struct Face
            {
                /// <summary>
                /// 人脸图片的唯一标识
                /// </summary>
                public string face_token;

                /// <summary>
                /// 人脸区域
                /// </summary>
                public Location location;

                /// <summary>
                /// 人脸置信度
                /// 范围[0,1]，代表这是一张人脸的概率，0最小、1最大
                /// </summary>
                public double face_probability;

                /// <summary>
                /// 三维旋转
                /// </summary>
                public Angle angle;

                /// <summary>
                /// 年龄
                /// </summary>
                public double age;

                /// <summary>
                /// 美丑打分
                /// 范围[0,100]，越大表示越美
                /// </summary>
                public double beauty;

                /// <summary>
                /// 人脸质量
                /// </summary>
                public Quality quality;

                /// <summary>
                /// 特征点
                /// 左眼中心、右眼中心、鼻尖、嘴中心
                /// </summary>
                public Vector2[] landmark;

                /// <summary>
                /// 72个特征点
                /// </summary>
                public Vector2[] landmark72;

                /// <summary>
                /// 区域信息
                /// </summary>
                public struct Location
                {
                    /// <summary>
                    /// 左
                    /// </summary>
                    public double left;

                    /// <summary>
                    /// 上
                    /// </summary>
                    public double top;

                    /// <summary>
                    /// 宽
                    /// </summary>
                    public double width;

                    /// <summary>
                    /// 高
                    /// </summary>
                    public double height;

                    /// <summary>
                    /// 旋转
                    /// </summary>
                    public double rotation;
                }

                /// <summary>
                /// 三维旋转
                /// </summary>
                public struct Angle
                {
                    /// <summary>
                    /// 左右旋转角[-90(左), 90(右)]
                    /// </summary>
                    public double yaw;

                    /// <summary>
                    /// 俯仰角度[-90(上), 90(下)]
                    /// </summary>
                    public double pitch;

                    /// <summary>
                    /// 平面内旋转角[-180(逆时针), 180(顺时针)]
                    /// </summary>
                    public double roll;
                }

                /// <summary>
                /// 质量
                /// </summary>
                public struct Quality
                {
                    /// <summary>
                    /// 遮挡比例
                    /// </summary>
                    public Occlusion occlusion;

                    /// <summary>
                    /// 人脸模糊程度
                    /// 范围[0,1]，0表示清晰，1表示模糊
                    /// </summary>
                    public double blur;

                    /// <summary>
                    /// 脸部区域光照程度
                    /// 范围[0,255],越大表示光照越好
                    /// </summary>
                    public double illumination;

                    /// <summary>
                    /// 人脸完整度
                    /// 0或1, 0为人脸溢出图像边界，1为人脸都在图像边界内
                    /// </summary>
                    public double completeness;

                    /// <summary>
                    /// 遮挡比例
                    /// </summary>
                    public struct Occlusion
                    {
                        /// <summary>
                        /// 左眼遮挡比例
                        /// </summary>
                        public double left_eye;

                        /// <summary>
                        /// 右眼遮挡比例
                        /// </summary>
                        public double right_eye;

                        /// <summary>
                        /// 鼻子遮挡比例
                        /// </summary>
                        public double nose;

                        /// <summary>
                        /// 嘴巴遮挡比例
                        /// </summary>
                        public double mouth;

                        /// <summary>
                        /// 左脸颊遮挡比例
                        /// </summary>
                        public double left_cheek;

                        /// <summary>
                        /// 右脸颊遮挡比例
                        /// </summary>
                        public double right_cheek;

                        /// <summary>
                        /// 下巴遮挡比例
                        /// </summary>
                        public double chin_contour;
                    }
                }
            }
        }
    }
}
